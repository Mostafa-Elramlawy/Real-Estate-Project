/*----------------------------------
    //------ SWIPPER ------//
-----------------------------------*/
// start swippper slider
BannerSlider: function () {
        var swiper = new Swiper('.banner_box_wrapper .swiper-container', {
            speed: 1000,
            loop: true,
            autoplay: false,
            slidesPerView: 1,
            navigation: {
                nextEl: '.banner_box_wrapper .swiper-button-next',
                prevEl: '.banner_box_wrapper .swiper-button-prev',
            },

        });
    },
    // End Swipper Slider;
