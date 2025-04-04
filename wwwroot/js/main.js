// Open & Close Cart

let cart = document.querySelector(".cart");

let closeCartIcon = document.querySelector(".close-cart");

let openCartIcon = document.querySelector(".top-nav-cart-icon");

let shopMoreBtn = document.querySelector(".shop-more-btn");


function openCart() {
  cart.classList.add("active");
}

function closeCart() {
  cart.classList.remove("active");
}

closeCartIcon.addEventListener("click", function () {
  closeCart();
});

openCartIcon.addEventListener("click", function () {
  openCart();
});

shopMoreBtn.addEventListener("click", function () {
  closeCart();
  window.location.href = "allproducts.html";
});

// Open & Close Menu

let linksMenu = document.querySelector("header .links ul");

let closeLinksMenuIcon = document.querySelector(".close-links-icon");

let openLinksMenuIcon = document.querySelector(".open-links-icon");

let menuBgOverlay = document.querySelector(".bg-overlay");

function openLinksMenu() {
  linksMenu.classList.add("active");
}
const medicineSwiper = new Swiper('.medicine-section-content .mySwiper', {
  slidesPerView: 3,
  spaceBetween: 20,
  navigation: {
    nextEl: '.swiper-button-next.medicine-swip-btn',
    prevEl: '.swiper-button-prev.medicine-swip-btn',
  },
});

function closeLinksMenu() {
  linksMenu.classList.remove("active");
}

closeLinksMenuIcon.addEventListener("click", function () {
  closeLinksMenu();
});

menuBgOverlay.addEventListener("click", function () {
  closeLinksMenu();
});

openLinksMenuIcon.addEventListener("click", function () {
  openLinksMenu();
});

// Add Items To Cart

let cartItemsContent = document.querySelector(".cart-items");

var allProducts = [];

let itemsPushedToCart = [];

let addToCart = function (id) {
  if (itemsPushedToCart.length == 0) {
    itemsPushedToCart.push(allProducts[id]);
    itemsPushedToCart[0].count = 1;
  } else {
    let productIndex = itemsPushedToCart.findIndex((item) => item.id === id);
    if (productIndex !== -1) {
      itemsPushedToCart[productIndex].count += 1;
    } else {
      itemsPushedToCart.push(allProducts[id]);
      itemsPushedToCart[itemsPushedToCart.length - 1].count = 1;
    }
  }

  getCartItems();
};

let totalHeaderPrice = document.querySelector(".total-price");
let totalHeaderCount = document.querySelector(".total-count");

let totalCartCount = document.querySelector(".cart-total-count");
let totalCartPrice = document.querySelector(".cart-total-price");

let getCartItems = function () {
  let cartItems = "";
  let price = 0;
  let count = 0;

  for (let i = 0; i < itemsPushedToCart.length; i++) {
    cartItems += `
       <div class="cart-item">
          <div>
            <img src="${itemsPushedToCart[i].img}" alt="product" />
          </div>

          <div class="cart-item-content">
            <h4>${itemsPushedToCart[i].name}</h4>
            <div class="cart-item-price-count">
              <p class="cart-item-price">$${
                itemsPushedToCart[i].price * itemsPushedToCart[i].count
              }</p>
              <p class="cart-item-count">Count: ${
                itemsPushedToCart[i].count
              }</p>
            </div>
          </div>

          <div class="delete-cart-item">
            <i class="fa-solid fa-trash-can" onclick="removeFromCart(${i})"></i>
          </div>
        </div> 
          `;

    count += itemsPushedToCart[i].count;
    price += itemsPushedToCart[i].count * itemsPushedToCart[i].price;
  }
  cartItemsContent.innerHTML = cartItems;

  totalHeaderPrice.innerHTML = "$" + price;

  totalHeaderCount.innerHTML = count;

  totalCartCount.innerHTML = `(${count} Items in Cart)`;

  totalCartPrice.innerHTML = "$" + price;
};

// Remove Item From Cart
let removeFromCart = (index) => {
  itemsPushedToCart.splice(index, 1);
  getCartItems();
};

// Show & Hide Back To Top Button
let backToTopBtn = document.querySelector(".back-to-top");

window.addEventListener("scroll", function (event) {
  if (window.scrollY > 200) {
    backToTopBtn.classList.add("active");
  } else {
    backToTopBtn.classList.remove("active");
  }
});

backToTopBtn.addEventListener("click", function () {
  window.scrollTo({
    top: 0,
    behavior: "smooth",
  });
});

// Item File Js Code

let bigImg = document.querySelector("#big-img");

let smallImgs = document.querySelectorAll(".small-imgs img");

smallImgs.forEach((img) => {
  img.addEventListener("click", function () {
    bigImg.src = img.src;
  });
});


// All Products Page Js Code

let filterMenu = document.querySelector(".filter");

let filterIcon = document.querySelector(".filter-btn");

filterIcon.addEventListener("click", function () {
  filterMenu.classList.toggle("active");
});

