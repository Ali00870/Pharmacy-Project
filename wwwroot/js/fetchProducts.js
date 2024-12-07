let medicineSectionProducts = document.querySelector(
  ".medicine-section-products"
);

fetch("js/items.json")
  .then((response) => response.json())
  .then((data) => {
    data.forEach((item) => {
      if (item.cat === "medicine") {
        medicineSectionProducts.innerHTML += getProduct(
          item.id,
          item.old_price,
          item.price,
          item.img,
          item.img_hover,
          item.name
        );
      }
    });
  });

function getProduct(id, oldPrice, price, img, imgHover, name) {
  return `
            <div class="product swiper-slide">
              ${
                oldPrice
                  ? `<div class="sale-percent">%${Math.floor(
                      ((oldPrice - price) / oldPrice) * 100
                    )}</div>`
                  : ""
              }
              <div class="icons">
                <span>
                  <i class="fa-solid fa-heart"></i>
                </span>

                <span>
                  <i class="fa-solid fa-cart-plus" onclick="addToCart(${id})"></i>
                </span>

                <span>
                  <i class="fa-solid fa-share"></i>
                </span>
              </div>

              <div class="product-img">
                <img src="${img}" alt="product" />

                <img
                  class="img-cover"
                  src="${imgHover}"
                  alt="product"
                />
              </div>

              <h3 class="product-name">
                <a href="../item.html"
                  >${name}</a>
              </h3>

              <div class="stars">
                <i class="fa-solid fa-star"></i>
                <i class="fa-solid fa-star"></i>
                <i class="fa-solid fa-star"></i>
                <i class="fa-solid fa-star"></i>
                <i class="fa-solid fa-star"></i>
              </div>

              <div class="price">
                <p><span>$ ${price}</span></p>
                ${oldPrice ? `<p class="old-price">$ ${oldPrice}</p>` : ""}
              </div>
            </div>
            `;
}
