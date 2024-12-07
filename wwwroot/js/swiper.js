// Side Bar Slider

var swiper = new Swiper(".swiper-slider", {
  pagination: {
    el: ".swiper-pagination",
    dynamicBullets: true,
    clickable: true,
  },
  autoplay: {
    delay: 3000,
  },
  loop: true,
});

// Sale & Mobile & Computer Slider
const medicineSwiper = new Swiper('.medicine-section-content .mySwiper', {
  slidesPerView: 3,
  spaceBetween: 20,
  navigation: {
    nextEl: '.swiper-button-next.medicine-swip-btn',
    prevEl: '.swiper-button-prev.medicine-swip-btn',
  },
});

var swiper = new Swiper(
  ".sale-section-content, .mobile-section-main-content, .computer-section-main-content",
  {
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
    slidesPerView: 5,
    spaceBetween: 20,
    autoplay: {
      delay: 3000,
    },
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    loop: true,

    breakpoints: {
      1500: {
        slidesPerView: 5,
      },
      1200: {
        slidesPerView: 4,
      },
      900: {
        slidesPerView: 3,
      },
      
      600: {
        slidesPerView: 2,
      },
      
      0: {
        slidesPerView: 1,
      },
    },
  }
);
