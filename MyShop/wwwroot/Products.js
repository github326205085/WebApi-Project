window.onload = function () {
    initializePage();
};

async function initializePage() {
    await viewAllProduct();
    await getCategories();
    updateBasketCount();
}

async function fetchJson(url) {
    const response = await fetch(url, { headers: { 'Content-Type': 'application/json' } });

    if (!response.ok) {
        throw new Error(`Network response was not ok: ${response.statusText}`);
    }

    return await response.json();
}

async function viewAllProduct() {
    try {
        const products = await fetchJson("api/Products");
        const template = document.getElementById("temp-card");

        products.forEach(product => viewOneProduct(product, template));
    } catch (error) {
        console.error('Error fetching product data:', error);
    }
}

function viewOneProduct(product, template) {
    const clone = document.importNode(template.content, true);
    clone.querySelector('h1').textContent = product.productName;
    clone.querySelector('.price').textContent = product.price;
    clone.querySelector('.description').textContent = product.description;
    clone.querySelector('.img-w img').src = product.image;

    const addToBasketButton = clone.querySelector('button');
    addToBasketButton.addEventListener('click', () => addToBasket(product));

    document.getElementById("PoductList").appendChild(clone);
}

async function getCategories() {
    try {
        const categories = await fetchJson("api/Categories");
        const template = document.getElementById("temp-category");

        categories.forEach(category => viewOneCategory(category, template));
    } catch (error) {
        console.error('Error fetching categories data:', error);
    }
}

function viewOneCategory(category, template) {
    const clone = document.importNode(template.content, true);
    clone.querySelector('.opt').id = category.categoryId;
    clone.querySelector('label').value = category.categoryName;
    clone.querySelector('label').setAttribute("for", category.categoryId);
    clone.querySelector('.OptionName').textContent = category.categoryName;

    clone.querySelector('.opt').addEventListener('change', filterProducts);
    document.getElementById("categoryList").appendChild(clone);
}

function addToBasket(product) {
    let basket = JSON.parse(sessionStorage.getItem("basket")) || [];

    const existingProduct = basket.find(item => item.book.productId === product.productId);

    if (existingProduct) {
        existingProduct.count += 1;
    } else {
        basket.push({ book: product, count: 1 });
    }

    sessionStorage.setItem("basket", JSON.stringify(basket));
    updateBasketCount();
}

async function filterProducts() {
    const minPrice = document.getElementById("maxPrice").value;
    const maxPrice = document.getElementById("minPrice").value;
    const nameSearch = document.getElementById("nameSearch").value;
    const categoryList = Array.from(document.querySelectorAll('#categoryList input[type="checkbox"]:checked')).map(cb => `&categoryIds=${cb.id}`).join('');

    try {
        const products = await fetchJson(`api/Products?desc=${nameSearch}&minPrice=${minPrice}&maxPrice=${maxPrice}${categoryList}`);

        const productList = document.getElementById("PoductList");
        productList.innerHTML = ''; 

        const template = document.getElementById("temp-card");
        products.forEach(product => viewOneProduct(product, template));
    } catch (error) {
        console.error('Error fetching filtered products:', error);
    }
}

function updateBasketCount() {
    const basket = JSON.parse(sessionStorage.getItem("basket")) || [];
    const count = basket.reduce((sum, item) => sum + item.count, 0);
    document.getElementById('ItemsCountText').textContent = count;
}


