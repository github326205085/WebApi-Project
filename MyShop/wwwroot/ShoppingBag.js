document.addEventListener('DOMContentLoaded', () => {
    viewAllShoppingBag();
});

function viewAllShoppingBag() {
    let totalSum = 0;
    let totalCount = 0;
    const shoppingBag = JSON.parse(sessionStorage.getItem("basket")) || [];
    const template = document.getElementById("temp-row");

    shoppingBag.forEach(prod => {
        viewOneProd(prod, template);
        totalSum += prod.book.price * prod.count;
        totalCount += prod.count;
    });

    updateTotals(totalSum, totalCount);
}

function viewOneProd(prod, template) {
    const clone = document.importNode(template.content, true);
    clone.querySelector('.itemName').textContent = prod.book.productName;
    clone.querySelector('.countColumn').textContent = prod.count;
    clone.querySelector('.price').textContent = (prod.book.price * prod.count).toFixed(2);
    clone.querySelector('.descriptionColumn').textContent = prod.book.description;

    const imgElement = clone.querySelector('.imageColumn .image');
    imgElement.style.backgroundImage = `url(${prod.book.image})`;

    clone.querySelector('button').addEventListener('click', () => removeItem(prod));
    document.getElementById("items").appendChild(clone);
}

function removeItem(prod) {
    let basket = JSON.parse(sessionStorage.getItem("basket")) || [];

    const existingProduct = basket.find(item => item.book.productId === prod.book.productId);

    if (existingProduct.count > 1) {
        existingProduct.count -= 1;
    } else {
        basket = basket.filter(item => item.book.productId !== prod.book.productId);
    }

    sessionStorage.setItem("basket", JSON.stringify(basket));
    refreshShoppingBag();
}

function refreshShoppingBag() {
    const parentElement = document.getElementById('items');
    parentElement.innerHTML = ''; // Clear all child elements

    viewAllShoppingBag();
}

async function placeOrder() {
    const shoppingBag = JSON.parse(sessionStorage.getItem("basket")) || [];
    const user = JSON.parse(sessionStorage.getItem("user"));
    const orderSum = parseFloat(document.getElementById('totalAmount').textContent);

    if (orderSum === 0) {
        alert("Your basket is still empty!!!");
        window.location.replace("Products.html?fromShoppingBag=1");
        return;
    }

    const orderItemDTOs = shoppingBag.map(item => ({
        productId: item.book.productId,
        quantity: item.count
    }));

    const orderDTO = {
        orderSum: orderSum,
        userId: user.userId,
        orderItemDTOs: orderItemDTOs
    };

    try {
        const response = await fetch("/api/Orders", {
            method: "POST",
            headers: { 'Content-Type': "application/json" },
            body: JSON.stringify(orderDTO)
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        alert(`Your order number ${data.orderId} has been made successfully!`);
        window.location.replace("Products.html?fromShoppingBag=1");

        sessionStorage.setItem("basket", JSON.stringify([]));
    } catch (error) {
        console.error('Error placing order:', error);
    }
}

function updateTotals(totalSum, totalCount) {
    document.getElementById('totalAmount').textContent = totalSum.toFixed(2);
    document.getElementById('itemCount').textContent = totalCount;
}