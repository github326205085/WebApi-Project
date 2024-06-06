document.addEventListener('DOMContentLoaded', function () {
    const switchers = [...document.querySelectorAll('.switcher')];

    switchers.forEach(item => {
        item.addEventListener('click', function (event) {
            switchers.forEach(switcher => switcher.parentElement.classList.remove('is-active'));
            this.parentElement.classList.add('is-active');
        });
    });

    const loginForm = document.getElementById('loginForm');
    const registerForm = document.getElementById('registerForm');
    const updateForm = document.getElementById('updateForm');

    if (loginForm && !loginForm.hasAttribute('data-listener-attached')) {
        loginForm.addEventListener('submit', handleSubmit.bind(null, login));
        loginForm.setAttribute('data-listener-attached', 'true');
    }

    if (registerForm && !registerForm.hasAttribute('data-listener-attached')) {
        registerForm.addEventListener('submit', handleSubmit.bind(null, addUser));
        registerForm.setAttribute('data-listener-attached', 'true');
    }

    if (updateForm && !updateForm.hasAttribute('data-listener-attached')) {
        updateForm.addEventListener('submit', handleSubmit.bind(null, updateUser));
        updateForm.setAttribute('data-listener-attached', 'true');
    }
});

async function handleSubmit(handler, event) {
    event.preventDefault();
    await handler();
}

async function login() {
    const user = buildUser('Login');

    try {
        const response = await fetch("/api/Users/login", {
            method: "POST",
            headers: { 'Content-Type': "application/json" },
            body: JSON.stringify(user)
        });

        if (!response.ok) throw new Error('Network response was not ok');

        const data = await response.json();
        console.log(data);
        sessionStorage.setItem("user", JSON.stringify(data));
        sessionStorage.setItem("basket", JSON.stringify([]));
        window.location.replace("Products.html");
    } catch (error) {
        console.error('Error logging in:', error);
    }
}

async function addUser() {
    const user = buildUser();

    if (!user.Email || !user.Password) {
        alert("User name and password required");
        return;
    }

    try {
        const response = await fetch("/api/Users", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        });

        const message = response.status === 400 ? "Email is not valid" :
            response.status === 204 ? "Password is weak" : '';

        if (message) {
            alert(message);
            return;
        }

        document.getElementById('loginSwitcher').click();
        return await response.json();
    } catch (error) {
        console.error('Error adding user:', error);
    }
}

async function updateUser() {
    const user = buildUser();
    user.UserId = Number(JSON.parse(sessionStorage.getItem("user")).userId);

    try {
        const response = await fetch("/api/Users", {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        });

        const message = response.status === 400 ? "Email is not valid" :
            response.status === 204 ? "Password is weak" : '';

        if (message) {
            alert(message);
            return;
        }

        window.location.replace("Products.html");
        return await response.json();
    } catch (error) {
        console.error('Error updating user:', error);
    }
}

async function passwordChecking() {
    const password = document.getElementById("psw").value;

    try {
        const response = await fetch("/api/Users/password", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(password)
        });

        if (!response.ok) throw new Error('Network response was not ok');

        const strength = await response.json();
        updatePasswordStrengthIndicator(strength);
    } catch (error) {
        console.error('Error checking password:', error);
    }
}

function buildUser(type = '') {
    return {
        Email: document.getElementById(`userName${type}`).value,
        Password: document.getElementById(`psw${type}`).value,
        FirstName: document.getElementById("firstName").value,
        LastName: document.getElementById("lastName").value
    };
}

function updatePasswordStrengthIndicator(strength) {
    const color = document.getElementById("passwordCheck");
    const colors = ["#edd1d1", "#DA9F9B", "#BC544C"];
    color.style.setProperty("background-color", colors[strength] || "#BC544C");
}