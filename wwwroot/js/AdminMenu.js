//$(function () {
//    $('#dropdown').hover(function () {
//        $('#dropdown').toggleClass('open');
//    })
//})

//$(function () {
//    $('[data-admin-menu]').hover(function () {
//        $('[data-admin-menu]').toggleClass('open');
//    })
//})

$(document).ready(function () {
    $('.navbar-light .dmenu').hover(function () {
        $(this).find('.sm-menu').first().stop(true, true).slideDown(150);
    }, function () {
        $(this).find('.sm-menu').first().stop(true, true).slideUp(105)
    });
});