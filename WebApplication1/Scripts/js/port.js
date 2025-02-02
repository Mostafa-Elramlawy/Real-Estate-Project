/*
Author       : Code-Theme
Template Name: MedicalHealth - HTML5 Template
Version      : 1.0
*/

jQuery(document).on('ready', function ($) {
    "use strict";

    /*---------------------------------
     //------ ANIMATE HEADER ------//
     ----------------------------------*/
    $(window).on('scroll', function () {
        var sticky = $(".sticky-header");
        var scroll = $(window).scrollTop();
        if (scroll < 265) {
            sticky.removeClass("sticky");
        } else {
            sticky.addClass("sticky");
        }
    });

    /*----------------------------------
    //------ SMOOTHSCROLL ------//
    -----------------------------------*/
    smoothScroll.init({
        speed: 1000, // Integer. How fast to complete the scroll in milliseconds
        offset: 85, // Integer. How far to offset the scrolling anchor location in pixels

    });

    /*----------------------------------
    //------ JQUERY SCROOLTOP ------//
    -----------------------------------*/
    var go = $(".go-up");
    $(window).on('scroll', function () {
        var scrolltop = $(this).scrollTop();
        if (scrolltop >= 50) {
            go.fadeIn();
        } else {
            go.fadeOut();
        }
    });

    /*----------------------------------
    //------ LIGHTCASE ------//
    -----------------------------------*/
    $('a[data-rel^=lightcase]').lightcase();

    /*----------------------------------
    //----- JQUERY IMAGES LOADED -----//
    -----------------------------------*/
    $('#container').imagesLoaded()
        .always(function (instance) {})
        .done(function (instance) {})
        .fail(function () {})
        .progress(function (instance, image) {
            var result = image.isLoaded ? 'loaded' : 'broken';
            console.log('image is ' + result + ' for ' + image.img.src);
        });

    /*----------------------------------
    //------ ISOTOPE GALLERY ------//
    -----------------------------------*/
    /* activate jquery isotope */
    $(window).on('load', function () {
        var $container = $('.portfolio-items').isotope({
            itemSelector: '.item',
            masonry: {
                columnWidth: '.col-xs-12'
            }
        });
    });
    // bind filter button click
    var filters = $('.filters-group ul li');
    filters.on('click', function () {
        filters.removeClass('active');
        $(this).addClass('active');
        var filterValue = $(this).attr('data-filter');
        // use filterFn if matches value
        $('.portfolio-items').isotope({
            filter: filterValue
        });
    });

}(jQuery));
