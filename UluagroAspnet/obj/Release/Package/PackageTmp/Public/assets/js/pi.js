$(document).ready(function () {

    const $body = $('body');

    function body_overflow_hidden() {
        $body.css('overflow', 'hidden');
    }

    function body_overflow_auto() {
        $body.css('overflow', '');
    }


    /*================================================================================================================*/
    // Drop Down
    /*================================================================================================================*/


    $('.drop').each(function () {
        const $drop = $(this)
        const $toggle = $('.drop-toggle', $drop)

        if ($drop.hasClass('select')) {
            if ($('.menu-item').hasClass('selected')) {
                $toggle.html($('.selected .abbr', $drop).clone())
            }
        }
    })

    document.addEventListener('click', function (e) {
        var elem = e.target

        if (!$(elem).is('.drop')
            && !$(elem).is('.drop .static')
            && !$(elem).is('.drop .static *')
            && !$(elem).is('.drop .drop-toggle')
            && !$(elem).is('.drop .drop-toggle *')) {
            $('.drop').removeClass('open')
            $('.drop-toggle').attr('aria-expanded', 'false')
        }

        if (!elem.classList.contains('drop-toggle')) {
            elem = elem.parentNode

            if (!elem.classList.contains('drop-toggle')) {
                return
            }
        }

        const $drop = $(elem.parentNode)
        const $toggle = $(elem, $drop)
        const $menu = $drop.find('.drop-menu')

        if ($drop.hasClass('open')) {
            $drop.removeClass('open')
            $toggle.attr('aria-expanded', 'false')
        } else {
            $('.drop').removeClass('open')

            $drop.addClass('open')
            $toggle.attr('aria-expanded', 'true')
        }

        $('.menu-item', $menu).on('click', function () {
            $menu.find('.menu-item').removeClass('selected')

            $(this).addClass('selected')

            $toggle.html($('.abbr', $(this)).clone())

            if ($drop.hasClass('no-href')) {
                return false
            }
        })
    })

    /*================================================================================================================*/
    // Modal
    /*================================================================================================================*/

    $(document).on('click', '[data-target-modal]', function () {

        const $this = $(this);
        const $target_modal = $this.data('target-modal');
        const $modal = $('[data-modal="' + $target_modal + '"]');

        if($modal.data('open-animation')){

            const $open_animation = $modal.data('open-animation');

            $('[data-modal="' + $target_modal + '"] > .modal-content').addClass('animated ' + $open_animation)
        }

        $modal.addClass('open');

        body_overflow_hidden();
        return false;

    });

    $(document).on('click', '[data-close]', function () {

        const $close = $(this).data('close');

        const $closed_modal = $('[data-modal="' + $close + '"]');

        const $close_animation = $closed_modal.attr('data-close-animation');
        const $open_animation = $closed_modal.data('open-animation');

        $('[data-modal="' + $close + '"] .modal-content').eq(0).addClass($close_animation).removeClass($open_animation);

        setTimeout(function(){

            $closed_modal.removeClass('open');

            $('[data-modal="' + $close + '"] .modal-content').eq(0).addClass($open_animation).removeClass($close_animation);

            $('[data-modal="' + $close + '"] .modal-overlay').remove();

            if (!$('.modal').hasClass('open')) {
                body_overflow_auto()
            }

        }, 200);

    });

    /*================================================================================================================*/
    // Accordion
    /*================================================================================================================*/

    $(".accordion").each(function () {

        const $accordion = $(this);
        const $panel = $('.accordion-panel', $accordion);
        const $title = $('.accordion-title', $accordion);
        const $content = $('.accordion-content', $accordion);

        $('.accordion-panel:not(.active) .accordion-content').hide();

        if($title.parent().hasClass('active')){
            $title.attr('aria-expanded', 'true')
        }

        $title.click(function () {

            const $attr = $(this).attr('data-target-accordion');

            if ($(this).parent().hasClass('active')) {

                $(this).parent().removeClass('active');

                $('[data-accordion="' + $attr + '"]').stop().slideUp(300);

                $(this).attr('aria-expanded', 'false');
            }
            else {

                $panel.removeClass('active');

                $(this).parent().addClass('active');

                $content.stop().slideUp(300);

                $('[data-accordion="' + $attr + '"]').stop().slideDown(300);

                $title.attr('aria-expanded', 'false');

                $(this).attr('aria-expanded', 'true');

            }

            return false;

        });

    });

    /*================================================================================================================*/
    // Collapse
    /*================================================================================================================*/

    $(document).on("click", '[data-target-collapse]', function () {
        const $toggle = $(this);
        const $collapse = $toggle.data('target-collapse');      

        $toggle.toggleClass('active');
        $('[data-collapse="' + $collapse + '"]').stop().slideToggle();

        activeCollase()

        function activeCollase() {
            if ($toggle.hasClass('active')) {
                $toggle.attr('aria-expanded', 'true');
            } else {
                $toggle.attr('aria-expanded', 'false');
                $('[data-collapse="' + $toggle + '"]').slideUp();
            }
        } activeCollase()
        
    })   

    /*================================================================================================================*/
    // Tab
    /*================================================================================================================*/

    $('[data-target-tab]').each(function () {

        const $this = $(this);
        const $target = $this.data('target-tab');

        if ($this.data('trigger')) {
            $this.hover(function () {
                tab();
            });
        }
        else {
            $this.click(function () {
                tab();
            });
        }

        function tab() {
            $('[data-tab="' + $target + '"]').addClass('active').siblings('.tab-panel').removeClass('active');
            $('[data-target-tab="' + $target + '"]').parent('li').addClass('active').siblings().removeClass('active');
        }

    });

    /*================================================================================================================*/
    // Rating stars
    /*================================================================================================================*/

    $(".rating-stars").each(function () {
        const span = $(this).find('span');
        span.width(span.attr("data-width") + '%');
    });

    /*================================================================================================================*/
    // Scroll To Target
    /*================================================================================================================*/

    $('.scroll-to').bind('click', function (event) {

        const $target = $(this);
        var $offsetTop;

        if ($target.data('as-ot')) {
            $offsetTop = $target.data('as-ot');
        }
        if ($(window).width() >= 1200) {
            if ($target.data('lg-ot')) {
                $offsetTop = $target.data('lg-ot');
            }
        }
        if ($(window).width() < 1200 && $(window).width() > 991) {
            if ($target.data('md-ot')) {
                $offsetTop = $target.data('md-ot');
            }
        }
        if ($(window).width() < 992 && $(window).width() > 767) {
            if ($target.data('sm-ot')) {
                $offsetTop = $target.data('sm-ot');
            }
        }
        if ($(window).width() < 768) {
            if ($target.data('xs-ot')) {
                $offsetTop = $target.data('xs-ot');
            }
        }

        $('html, body').stop().animate({scrollTop: $($target.attr('href')).offset().top - $offsetTop}, 1000);
        event.preventDefault();
    });

    /*================================================================================================================*/
    // target = _blank
    /*================================================================================================================*/

    function target_blank() {
        $("a[rel*='external']").attr("target", "_blank");
    }target_blank();

    /*================================================================================================================*/
    // Scrolling Menu
    /*================================================================================================================*/

    const pageUrl = window.location;
    const path = window.location.pathname;
    const page = path.split("/").pop();

    $('.scrolling-menu').each(function () {

        const $scrollingMenu = $(this);

        $scrollingMenu.find('li a').filter(function () {
            return this.href == pageUrl;
        }).parent().addClass('active');

        $scrollingMenu.append($('<li class="line"></li>'));

        function animateScrollLeft(elem, value, duration) {
            const start = +new Date,
                currentValue = elem.scrollLeft;

            (function (a, b) {
                return function _animate() {
                    const now = +new Date - start,
                        progress = now / duration,
                        result = (a - b) * progress + b;

                    elem.scrollLeft = progress < 1 ? result : a;

                    if (progress < 1) {
                        setTimeout(_animate, 10);
                    }
                };
            })(value, currentValue)();
        }

        function scrollToActive() {
            const elem = $scrollingMenu.find('li.active')[0],
                parent = elem.parentElement,
                line = $scrollingMenu.find('.line')[0];

            animateScrollLeft(parent, elem.offsetLeft - parent.offsetWidth / 2 + elem.offsetWidth / 2, 0);

            line.style.left = elem.offsetLeft + 'px';
            line.style.width = elem.offsetWidth + 'px';

            document.removeEventListener('DOMContentLoaded', scrollToActive);
        }

        scrollToActive();

        document.addEventListener('DOMContentLoaded', scrollToActive);

        document.addEventListener('click', function (e) {
            var button = e.target,
                scrollingMenu = button.parentElement;

            if ($(scrollingMenu)) {
                if (!$(scrollingMenu).hasClass('scrolling-menu')) {
                    scrollingMenu = scrollingMenu.parentElement;

                    if (!$(scrollingMenu).hasClass('scrolling-menu')) {
                        scrollingMenu = scrollingMenu.parentElement;

                        button = button.parentElement;

                        if (!$(scrollingMenu).hasClass('scrolling-menu')) {
                            return;
                        }
                    }
                }
            }

            button = button.parentElement;

            const active = scrollingMenu.querySelector('.active'),
                line = scrollingMenu.querySelector('.line');

            if (active) {
                active.classList.remove('active');
            }

            button.classList.add('active');

            animateScrollLeft(scrollingMenu, button.offsetLeft - scrollingMenu.offsetWidth / 2 + button.offsetWidth / 2, 200);

            line.style.left = button.offsetLeft + 'px';
            line.style.width = button.offsetWidth + 'px';

            if ($scrollingMenu.hasClass('no-url')) {
                e.preventDefault();
            }
        });

    });

    /*================================================================================================================*/
    /*================================================================================================================*/

});
