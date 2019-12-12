//-----------------------------------------------------------Davis data---------------------------------------------
$(document).on("click", '.agrostation', function () {
    $.ajax({
        url: "/JSON/Davis",
        method: "get",
        data: {
            id: $(this).data("id")
        },
        beforeSend: function () {
            $("#loadingWheather").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            var davisdata = JSON.parse(response);
            var ctx_temp = document.getElementById('myChart_temp');
            var ctx_solar_rad = document.getElementById('myChart_solar_rad');
            var ctx_humidity = document.getElementById('myChart_humidity');
            var ctx_ET = document.getElementById('myChart_ET');

            $('.chart-panel').text(davisdata.davis_current_observation.station_name);

            //Temperature
            var myChart_temp = new Chart(ctx_temp, {
                type: 'bar',
                data: {
                    labels: ['Xarici istilik', 'Külək', 'İstilik əmsalı', 'Şeh nöqtəsi'],
                    datasets: [{
                        label: 'Göstərici (°C)',
                        data: [davisdata.temp_c, davisdata.windchill_c, davisdata.heat_index_c, davisdata.dewpoint_c],
                        backgroundColor: [
                            'rgb(196, 39, 40)',
                            'rgb(50, 136, 194)',
                            'rgb(233, 127, 36)',
                            'rgb(56, 145, 109)'
                        ],
                        borderColor: [
                            'rgb(196, 39, 40)',
                            'rgb(50, 136, 194)',
                            'rgb(233, 127, 36)',
                            'rgb(56, 145, 109)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    legend: {
                        labels: {
                            fontSize: 40
                        }
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true,
                                suggestedMax: 50
                            }
                        }]
                    },
                    title: {
                        display: true,
                        text: 'Temperatur',
                        fontSize: 35
                    },
                    tooltips: {
                        mode: 'label',
                        callbacks: {
                            label: function (tooltipItems, data) {
                                return tooltipItems.yLabel + ' °C';
                            }
                        }
                    }
                }
            });

            //Solar Radiation
            var myChart_solar_rad = new Chart(ctx_solar_rad, {
                type: 'doughnut',
                data: {
                    labels: ['Günəş radiasiyası ' + davisdata.davis_current_observation.solar_radiation + '', 'Yüksək radiasiya ' + davisdata.davis_current_observation.solar_radiation_day_high + ''],
                    datasets: [{
                        label: '# of Votes',
                        data: [davisdata.davis_current_observation.solar_radiation, davisdata.davis_current_observation.solar_radiation_day_high - davisdata.davis_current_observation.solar_radiation],
                        backgroundColor: [
                            'rgb(196, 39, 40)',
                            'gray'
                        ],
                        borderColor: [
                            'rgb(196, 39, 40)',
                            'gray'
                        ],
                        borderWidth: 1,
                    }]
                },
                options: {
                    legend: {
                        labels: {
                            fontSize: 40
                        }
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true,
                                suggestedMax: davisdata.davis_current_observation.solar_radiation_day_high
                            }
                        }]
                    },
                    title: {
                        display: true,
                        text: 'Günəş radiasiyası',
                        fontSize: 35
                    }
                }
            });

            //Humidity
            var myChart_humidity = new Chart(ctx_humidity, {
                type: 'doughnut',
                data: {
                    labels: ['Hazırkı nəmişlik ' + davisdata.relative_humidity + '', 'Yüksək nəmişlik ' + davisdata.davis_current_observation.relative_humidity_day_high + ''],
                    datasets: [{
                        label: 'Göstərici',
                        data: [davisdata.relative_humidity, davisdata.davis_current_observation.relative_humidity_day_high - davisdata.relative_humidity],
                        backgroundColor: [
                            'rgb(41, 181, 116)',
                            'gray'
                        ],
                        borderColor: [
                            'rgb(41, 181, 116)',
                            'gray'
                        ],
                        borderWidth: 1,
                    }]
                },
                options: {
                    legend: {
                        labels: {
                            fontSize: 40
                        }
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true,
                                suggestedMax: davisdata.davis_current_observation.relative_humidity_day_high
                            }
                        }]
                    },
                    title: {
                        display: true,
                        text: 'Nəmişlik',
                        fontSize: 35
                    }
                }
            });

            //ET
            var myChart_ET = new Chart(ctx_ET, {
                type: 'bar',
                data: {
                    labels: ['Günlük', 'Aylıq', 'İllik'],
                    datasets: [{
                        label: 'Göstərici (in)',
                        data: [davisdata.davis_current_observation.et_day, davisdata.davis_current_observation.et_month, davisdata.davis_current_observation.et_year],
                        backgroundColor: [
                            'rgb(41, 181, 116)',
                            'rgb(41, 181, 116)',
                            'rgb(50, 136, 194)'
                        ],
                        borderColor: [
                            'rgb(41, 181, 116)',
                            'rgb(41, 181, 116)',
                            'rgb(50, 136, 194)'
                        ],
                        borderWidth: 1,
                    }]
                },
                options: {
                    legend: {
                        labels: {
                            fontSize: 40
                        }
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true,
                                suggestedMax: 20
                            }
                        }]
                    },
                    title: {
                        display: true,
                        text: 'Buxarlanma',
                        fontSize: 35
                    }
                }
            });
        },
        complete: function () {
            $("#loadingWheather").css("display", "none");
        }
    })
})

//---------------------------------------When doc is ready show temperature in the header------------------------------------
$(document).ready(function () {
    $.ajax({
        url: "/JSON/Davis",
        method: "get",
        data: {
            id: 1
        },
        type: "JSON",
        success: function (response) {
            var davisdata = JSON.parse(response);

            var m_temp = parseFloat(davisdata.temp_c);

            $('.morning_temp').text('Qax - ' + m_temp.toFixed(1) + '°C');
        }
    })
});

//----------------------------------------------------------End Davis--------------------------------------------------------

//---------------------------------------------------------Avto Ordered Loader-----------------------------------------------
//Normal Load
$(".car #menu-13").on("click", function () {
    $.ajax({
        url: "/JSON/AvtoLoad",
        method: "get",
        data: {
            skip: 0,
            type: "ordered"
        },
        beforeSend: function () {
            $("#data_loader_avto").css("display", "none");
            $("#loadingavto").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            $('#data_loader_avto').html("");
            $('#data_loader_avto').append(response);
        },
        complete: function () {
            $("#data_loader_avto").css("display", "block");
            $("#loadingavto").css("display", "none");
        }
    })
})

//Back To Load
$("#back_to_avto").on("click", function () {
    $.ajax({
        url: "/JSON/AvtoLoad",
        method: "get",
        data: {
            skip: 0,
            type: "ordered"
        },
        beforeSend: function () {
            $("#data_loader_avto").css("display", "none");
            $("#loadingavto").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            $('#data_loader_avto').html("");
            $('#data_loader_avto').append(response);
        },
        complete: function () {
            $("#data_loader_avto").css("display", "block");
            $("#loadingavto").css("display", "none");
        }
    })
    $(this).css('display', 'none');
    $('#arxiv_avto').css('display', 'inline-block');
})

$("#loader_close").on("click", function () {
    $("#arxiv_avto").css('display', 'inline-block');
    $("#back_to_avto").css('display', 'none');
})
//---------------------------------------------------------End Loader------------------------------------------------

//-------------------------------------------------------Pagination Ordered Avto---------------------------------------------
$(document).on("click", "#page_nav_ordered .page_link", function () {
    $.ajax({
        url: "/JSON/AvtoLoad",
        method: "get",
        data: {
            skip: $(this).data("skip"),
            type: "ordered"
        },
        beforeSend: function () {
            $("#data_loader_avto").css("display", "none");
            $("#loadingavto").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            $('#data_loader_avto').html("");
            $('#data_loader_avto').append(response);
        },
        complete: function () {
            $("#data_loader_avto").css("display", "block");
            $("#loadingavto").css("display", "none");
        }
    })
    return false;
})
//--------------------------------------------------------End Pagination---------------------------------------------

//-------------------------------------------------------Pagination Avto Seacrh--------------------------------------
$(document).on("click", "#page_nav_searched .page_link", function () {
    $.ajax({
        url: "/JSON/AvtoSearch",
        method: "get",
        data: {
            skip: $(this).data("skip"),
            query: $("#search").val()
        },
        beforeSend: function () {
            $("#data_loader_avto").css("display", "none");
            $("#loadingavto").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            $('#data_loader_avto').html("");
            $('#data_loader_avto').append(response);
        },
        complete: function () {
            $("#data_loader_avto").css("display", "block");
            $("#loadingavto").css("display", "none");
        }
    })
    return false;
})
//--------------------------------------------------------End Pagination---------------------------------------------

//------------------------------------------------------------Search-------------------------------------------------
$("#search").on("keyup", function (e) {
    if (e.which == 13) {
        $.ajax({
            url: "/JSON/AvtoSearch",
            method: "get",
            data: {
                skip: 0,
                query: $(this).val()
            },
            beforeSend: function () {
                $("#data_loader_avto").css("display", "none");
                $("#loadingavto").css("display", "block");
            },
            type: "JSON",
            success: function (response) {
                $('#data_loader_avto').html("");
                $('#data_loader_avto').append(response);
            },
            complete: function () {
                $("#data_loader_avto").css("display", "block");
                $("#loadingavto").css("display", "none");
            }
        })
    }
})
//-------------------------------------------------------------End---------------------------------------------------

//-------------------------------------------------------Avto Arxiv Loader-------------------------------------------
$("#arxiv_avto").on("click", function () {
    $.ajax({
        url: "/JSON/AvtoLoad",
        method: "get",
        data: {
            skip: 0,
            type: "normal"
        },
        beforeSend: function () {
            $("#data_loader_avto").css("display", "none");
            $("#loadingavto").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            $('#data_loader_avto').html("");
            $('#data_loader_avto').append(response);
        },
        complete: function () {
            $("#data_loader_avto").css("display", "block");
            $("#loadingavto").css("display", "none");
        }
    })
    $("#back_to_avto").css('display', 'inline-block');
    $(this).css('display', 'none');
})
//---------------------------------------------------------End Loader------------------------------------------------

//---------------------------------------------------Pagination Arxiv Avto-------------------------------------------
$(document).on("click", "#page_nav .page_link", function () {
    $.ajax({
        url: "/JSON/AvtoLoad",
        method: "get",
        data: {
            skip: $(this).data("skip"),
            type: "normal"
        },
        beforeSend: function () {
            $("#data_loader_avto").css("display", "none");
            $("#loadingavto").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            $('#data_loader_avto').html("");
            $('#data_loader_avto').append(response);
        },
        complete: function () {
            $("#data_loader_avto").css("display", "block");
            $("#loadingavto").css("display", "none");
        }
    })
    return false;
})
//--------------------------------------------------------End Pagination---------------------------------------------

//---------------------------------------------------------Pop up ArcGis---------------------------------------------


$("#mapDiv").mouseover(function () {
    var img = document.querySelectorAll("#graphicsLayer3_layer image");

    for (var i = 0; i < img.length; i++) {
        img[i].classList.add("agrostation");
        img[i].setAttribute('data-target-modal', 'analytics-forecast');
        img[i].setAttribute('data-id', '' + (i + 1) + '');
    }
});

//--------------------------------------------------------------END--------------------------------------------------

//----------------------------------------------------------Work Control---------------------------------------------
//Load
$(".hr #menu-work-control").on("click", function () {
    $.ajax({
        url: "/JSON/WorkerLoad",
        method: "get",
        data: {
            skip: 0,
            id: $("#slct_wrkr").val(),
            start: $("#date_start").val(),
            finish: $("#date_finish").val()
        },
        beforeSend: function () {
            $("#loadingwork").css("display", "block");
            $('#data_loader_workers').css("display", "none");
        },
        type: "JSON",
        success: function (response) {
            $("#data_loader_workers").html("");
            $("#data_loader_workers").append(response);
        },
        complete: function () {
            $("#loadingwork").css("display", "none");
            $('#data_loader_workers').css("display", "block");
        }
    });
});

//Pagination
$(document).on("click", "#page_nav_ordered_workerdata .page_link", function () {
    $.ajax({
        url: "/JSON/WorkerLoad",
        method: "get",
        data: {
            skip: $(this).data("skip"),
            id: $("#slct_wrkr").val(),
            start: $("#date_start").val(),
            finish: $("#date_finish").val()
        },
        beforeSend: function () {
            $("#loadingwork").css("display", "block");
            $('#data_loader_workers').css("display", "none");
        },
        type: "JSON",
        success: function (response) {
            $('#data_loader_workers').html("");
            $('#data_loader_workers').append(response);
        },
        complete: function () {
            $("#loadingwork").css("display", "none");
            $('#data_loader_workers').css("display", "block");
        }
    })
    return false;
})

//Load worker for Profession
$("#slct_prof").change(function () {
    $.ajax({
        url: "/JSON/LoadWorkerForProfession",
        method: "get",
        data: { id: $(this).val() },
        beforeSend: function () {
            $("#slct_wrkr").attr("disabled", true);
        },
        type: "JSON",
        success: function (response) {
            $("#slct_wrkr option").remove();
            $("#slct_wrkr").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#slct_wrkr").append("<option value='" + response[i].ID + "'>" + response[i].Name + ' ' + response[i].Surname + "</option>");
                }
            }
        },
        complete: function () {
            $("#slct_wrkr").removeAttr("disabled");
        }
    })
});

//Load data for worker selection
$("#slct_wrkr").change(function () {
    $.ajax({
        url: "/JSON/WorkerLoad",
        method: "get",
        data: {
            id: $(this).val(),
            skip: 0,
            start: $("#date_start").val(),
            finish: $("#date_finish").val()
        },
        beforeSend: function () {
            $("#loadingwork").css("display", "block");
            $('#data_loader_workers').css("display", "none");
        },
        type: "JSON",
        success: function (response) {
            $("#data_loader_workers").html("");
            $("#data_loader_workers").append(response)
        },
        complete: function () {
            $("#loadingwork").css("display", "none");
            $('#data_loader_workers').css("display", "block");
        }
    })
});

//Date range selection
$(function () {
    $("#date_ranger").daterangepicker({
        opens: 'center'
    }, function (start, end) {
        $("#date_start").val(start.format('YYYY-MM-DD'));
        $("#date_finish").val(end.format('YYYY-MM-DD'));
        $.ajax({
            url: "/JSON/WorkerLoad",
            method: "get",
            data: {
                id: $("#slct_wrkr").val(),
                skip: 0,
                start: start.format('YYYY-MM-DD'),
                finish: end.format('YYYY-MM-DD'),
            },
            beforeSend: function () {
                $("#loadingwork").css("display", "block");
                $('#data_loader_workers').css("display", "none");
            },
            type: "JSON",
            success: function (response) {
                $("#data_loader_workers").html("");
                $("#data_loader_workers").append(response)
            },
            complete: function () {
                $("#loadingwork").css("display", "none");
                $('#data_loader_workers').css("display", "block");
            }
        })
    });
});

//Reset filter
$("#reset_filter").on("click", function () {
    $("#slct_wrkr").val(null);
    $("#date_start").val(null);
    $("#date_finish").val(null);
    $.ajax({
        url: "/JSON/WorkerLoad",
        method: "get",
        data: {
            skip: 0,
            id: $("#slct_wrkr").val(),
            start: $("#date_start").val(),
            finish: $("#date_finish").val()
        },
        beforeSend: function () {
            $("#loadingwork").css("display", "block");
            $('#data_loader_workers').css("display", "none");
        },
        type: "JSON",
        success: function (response) {
            $("#data_loader_workers").html("");
            $("#data_loader_workers").append(response);
        },
        complete: function () {
            $("#loadingwork").css("display", "none");
            $('#data_loader_workers').css("display", "block");
        }
    });
});
//--------------------------------------------------------------END--------------------------------------------------

//----------------------------------------------------------Garage---------------------------------------------------
$("#garage_opener").on("click", function () {
    $.ajax({
        url: "/JSON/GarageLoad",
        method: "get",
        data: {
            skip: 0
        },
        beforeSend: function () {
            $("#garage_con").css("display", "none");
            $("#loadingGarage").css("display", "block");
        },
        type: "JSON",
        success: function (response) {
            $('#garage_con').html("");
            $('#garage_con').append(response);
        },
        complete: function () {
            $("#garage_con").css("display", "block");
            $("#loadingGarage").css("display", "none");
        }
    })
})

//-------------Pagination-------------
$(document).on("click", "#page_garage_ordered .page_link", function () {
    $.ajax({
        url: "/JSON/GarageLoad",
        method: "get",
        data: {
            skip: $(this).data("skip")
        },
        beforeSend: function () {
            $("#loadingGarage").css("display", "block");
            $('#garage_con').css("display", "none");
        },
        type: "JSON",
        success: function (response) {
            $('#garage_con').html("");
            $('#garage_con').append(response);
        },
        complete: function () {
            $("#loadingGarage").css("display", "none");
            $('#garage_con').css("display", "block");
        }
    })
    return false;
})
//-----------------------------------------------------------END-----------------------------------------------------

//-----------------------------------------------------------MAP-----------------------------------------------------
var map;
var carlogdata = [];
var interval;

require([
    "esri/map",
    "esri/geometry/Point",
    "esri/symbols/SimpleMarkerSymbol",
    "esri/graphic",
    "esri/InfoTemplate",
    "esri/layers/ArcGISDynamicMapServiceLayer",
    "esri/layers/FeatureLayer",
    "esri/dijit/BasemapToggle",
    "esri/symbols/SimpleFillSymbol",
    "esri/symbols/SimpleLineSymbol",
    "esri/tasks/IdentifyTask",
    "esri/tasks/IdentifyParameters",
    "esri/dijit/Popup",
    "esri/dijit/PopupTemplate",
    "dojo/_base/array",
    "esri/Color",
    "dojo/dom-construct",
    "dojo/domReady!"
],
    function (
    Map, Point, SimpleMarkerSymbol, Graphic, InfoTemplate, ArcGISDynamicMapServiceLayer, FeatureLayer, BasemapToggle, SimpleFillSymbol,
    SimpleLineSymbol, IdentifyTask, IdentifyParameters, Popup, PopupTemplate,
    arrayUtils, Color, domConstruct
) {
    var identifyTask, identifyParams;

    var popup = new Popup({
        fillSymbol: new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID,
                new Color([255, 0, 0]), 2), new Color([255, 255, 0, 0.25]))
    }, domConstruct.create("div"));

    map = new Map("mapDiv", {
        sliderOrientation: "vertical",
        sliderPosition: "bottom-left",
        logo: false,
        basemap: "satellite",
        center: [46.8076599, 41.2297739],
        zoom: 13,
        infoWindow: popup
    });

    $(document).on("click", ".socialsForClick", function ($event) {
        if ($event.target.innerText === 'Google') {
            var toggle = new BasemapToggle({
                map: map,
                basemap: "topo"
            });
        } else if ($event.target.innerText === 'Bing') {
            var toggle = new BasemapToggle({
                map: map,
                basemap: "satellite"
            });
        } else if ($event.target.innerText === 'Yandex') {
            var toggle = new BasemapToggle({
                map: map,
                basemap: "osm"
            });
        }
        toggle.startup();
    });

    map.on("load", mapReady);

    //map.on("load", mapLoaded);
    //map.on("load", function () {
    //    setTimeout(ajaxrequest, 25000);
    //});

    var isopen = true;

    //Appears or disappears cars on the map
    $(document).on("click", "#map_car_show", function () {
        isopen = !isopen;
        clearInterval(interval);

        if (isopen == false) {
            $(this).children().removeClass("fa-toggle-off");
            $(this).children().addClass("fa-toggle-on");
            $(this).children().css("color", "#009eff");
            mapLoaded();
        }
        else {
            $(this).children().removeClass("fa-toggle-on");
            $(this).children().addClass("fa-toggle-off");
            $(this).children().css("color", "#ffffff99");
            map.graphics.clear();
            return;
        }
    });

    //Centeralize and zoom the selected car on the map
    $(document).on("click", ".all_cars .show-loc", function () {
        clearInterval(interval);
        if (isopen == false) {
            $.ajax({
                url: "/MapAJAX/GetCarLocation",
                method: "get",
                data: {
                    terminal: $(this).data("terminal")
                },
                type: "JSON",
                success(response) {
                    map.centerAndZoom(new Point(response.longitude, response.latitude),15);
                }
            })
        }
    });

    //Navigation animation
    var graphic;
    
    function mapLoaded() {
        if (isopen == false) {
            $.ajax({
                url: "/MapAJAX/Loader",
                method: "get",
                type: "GET",
                success: function (response) {
                    clearInterval(interval);
                    map.graphics.clear();

                    var iconPath = "M29.395,0H17.636c-3.117,0-5.643,3.467-5.643,6.584v34.804c0,3.116,2.526,5.644,5.643,5.644h11.759   c3.116,0,5.644-2.527,5.644-5.644V6.584C35.037,3.467,32.511,0,29.395,0z M34.05,14.188v11.665l-2.729,0.351v-4.806L34.05,14.188z    M32.618,10.773c-1.016,3.9-2.219,8.51-2.219,8.51H16.631l-2.222-8.51C14.41,10.773,23.293,7.755,32.618,10.773z M15.741,21.713   v4.492l-2.73-0.349V14.502L15.741,21.713z M13.011,37.938V27.579l2.73,0.343v8.196L13.011,37.938z M14.568,40.882l2.218-3.336   h13.771l2.219,3.336H14.568z M31.321,35.805v-7.872l2.729-0.355v10.048L31.321,35.805z";
                    var initColor = "#ce641d";
                    for (var i = 0; i < response.length; i++) {
                        var infoTemplate = new InfoTemplate();

                        infoTemplate.setTitle(`Plaka: ${response[i].name}`);

                        infoTemplate.setContent(`<b>Məsafə: </b>${response[i].distance}<br/>`

                            + `<b>Sürət: </b>${response[i].speed}<br/>`);

                        graphic = new Graphic(
                            new Point(response[i].longitude, response[i].latitude),
                            createSymbol(iconPath, initColor, response[i].direction)
                        );
                        graphic.setInfoTemplate(infoTemplate);
                        map.graphics.add(graphic);
                    }
                },
                complete: function () {
                    setTimeout(mapLoaded, 3000);
                }
            });
        }
    }

    //Car Log Data Animation
    $(document).on("click", ".play_car_animation", function () {
        $("#data_load_car_info").html("");
        clearInterval(interval);
        var i = 0;
        interval = setInterval(function () {
            map.graphics.clear();

            var iconPath = "M29.395,0H17.636c-3.117,0-5.643,3.467-5.643,6.584v34.804c0,3.116,2.526,5.644,5.643,5.644h11.759   c3.116,0,5.644-2.527,5.644-5.644V6.584C35.037,3.467,32.511,0,29.395,0z M34.05,14.188v11.665l-2.729,0.351v-4.806L34.05,14.188z    M32.618,10.773c-1.016,3.9-2.219,8.51-2.219,8.51H16.631l-2.222-8.51C14.41,10.773,23.293,7.755,32.618,10.773z M15.741,21.713   v4.492l-2.73-0.349V14.502L15.741,21.713z M13.011,37.938V27.579l2.73,0.343v8.196L13.011,37.938z M14.568,40.882l2.218-3.336   h13.771l2.219,3.336H14.568z M31.321,35.805v-7.872l2.729-0.355v10.048L31.321,35.805z";
            var initColor = "#ce641d";

            var infoTemplate = new InfoTemplate();

            infoTemplate.setTitle(`Plaka: ${carlogdata[i].name}`);

            infoTemplate.setContent(`<b>Məsafə: </b>${carlogdata[i].distance}<br/>`

                + `<b>Sürət: </b>${carlogdata[i].speed}<br/>`);

            graphic = new Graphic(
                new Point(carlogdata[i].longitude, carlogdata[i].latitude),
                createSymbol(iconPath, initColor, carlogdata[i].direction)
            );
            graphic.setInfoTemplate(infoTemplate);
            map.centerAt(new Point(carlogdata[i].longitude, carlogdata[i].latitude))
            map.graphics.add(graphic);
            i++;

            if (i > carlogdata.length - 1) {
                clearInterval(interval);
            }
        }, 500)
    });

    function createSymbol(path, color, angle) {
        var markerSymbol = new esri.symbol.SimpleMarkerSymbol();
        markerSymbol.setPath(path);
        markerSymbol.setAngle(angle);
        markerSymbol.setColor(new dojo.Color(color));
        markerSymbol.setSize("30");
        return markerSymbol;
    }

    var parcelsURL = "http://213.154.5.139:8021/arcgis/rest/services/AgroParkDetail/MapServer";

    var featureLayerStation = new FeatureLayer("http://213.154.5.139:8021/arcgis/rest/services/AgroParkDetail/FeatureServer/0", {
        mode: FeatureLayer.MODE_ONDEMAND,
        outFields: ["*"],
        opacity: 0.9
    });

    var popupTemplate = new InfoTemplate();
    popupTemplate.setTitle("Ətraflı məlumat")
    popupTemplate.setContent("<b>Becərilən bitki: </b>${EKIN}<br/>"
        + "<b>Sortu: </b>${NOV}<br/>"
        + "<b>Sahə (ha): </b>${AREAS}<br/>"
        + "<b>Xəstəlik riskləri: </b><a title='Xəstəlik riskləri' class='risk-btn' data-risk='${OBJECTID}' data-target-modal='risk-modal' href='javascript: void(0);'>Ətraflı</a>"
    );

    var featureLayer = new FeatureLayer("http://213.154.5.139:8021/arcgis/rest/services/AgroParkDetail/FeatureServer/1", {
        mode: FeatureLayer.MODE_ONDEMAND,
        outFields: ["*"],
        opacity: 0.9
    });
    featureLayer.setInfoTemplate(popupTemplate);

    var dynamicOrthoLayer = new ArcGISDynamicMapServiceLayer("http://213.154.5.139:8021/arcgis/rest/services/RasterDataAgroPark/MapServer");

    dynamicOrthoLayer.setVisibleLayers(-1);
    map.addLayer(dynamicOrthoLayer);
    map.addLayer(featureLayer);

    map.addLayer(featureLayerStation);

    $(document).on("click", ".map-history", function ($event) {

        var dyvisibleLayerIds = [];
        // dyvisibleLayerIds.push($event.target.alt);

        dyvisibleLayerIds.push(this.getAttribute("data-nov"));
        dynamicOrthoLayer.setVisibleLayers(dyvisibleLayerIds);

    });

    function mapReady() {
        //map.on("click", executeIdentifyTask);
        //create identify tasks and setup parameters
        identifyTask = new IdentifyTask(parcelsURL);

        identifyParams = new IdentifyParameters();
        identifyParams.tolerance = 3;
        identifyParams.returnGeometry = true;
        //identifyParams.layerIds = [22, 25];
        identifyParams.layerOption = IdentifyParameters.LAYER_OPTION_ALL;
        identifyParams.width = map.width;
        identifyParams.height = map.height;
    }



    $("#_crops").on("click", function () {
        ShowGardenAndCrops("1")
    });

    $("#_gardens").on("click", function () {
        ShowGardenAndCrops("2")
    });
    $(".showmap").on("click", function () {
        console.log(this.innerText)
        // NOV
        featureLayer.setDefinitionExpression("EKIN = N'" + this.innerText + "'");
        map.addLayer(featureLayer);
    })
    $(".showmapkind").on("click", function () {
        console.log(this.innerText)
        // SORTLAR
        featureLayer.setDefinitionExpression("NOV = N'" + this.innerText + "'");
        map.addLayer(featureLayer);
    })


    function ShowGardenAndCrops(value) {
        // featureLayer.setVisibleLayers(-1);

        // denli ve baglar
        featureLayer.setDefinitionExpression("ID = '" + value + "'");
        // NOV
        //featureLayer.setDefinitionExpression("EKIN = N'BUĞDA'");

        // SORTLAR
        //featureLayer.setDefinitionExpression("NOV = N'Aran R1'");
        map.addLayer(featureLayer);
    }

    });
//-------------------------------------------------------------------------------------------------------------------

//---------------------------------------------------Load Cars-------------------------------------------------------
$(document).ready(function () {
    $.ajax({
        url: "/MapAJAX/Loader",
        method: "get",
        type: "GET",
        success: function (response) {
            for (var i = 0; i < response.length; i++) {
                $(".all_cars .sub-menu").append(
                    `<li role="presentation" class="clear">
                        <a href="#" role="menuitem" class="sub-menu-title text-uppercase bold float-left" title="${response[i].name}">${response[i].name}</a>
                        <span class="menu-toggle pointer float-left text-center show-loc" rel="car" data-terminal="${response[i].terminal}">
                            <i aria-hidden="true" class="icon-eye v-align-middle tr-3s"></i>
                        </span>
                    </li>`
                );
                $("#slct_car").append(`<option value="${response[i].terminal}">${response[i].name}</option>`);
            }
        }
    });
});

$(function () {
    $("#car_date_ranger").daterangepicker({
        opens: 'center'
    }, function (start, end) {
        $("#car_date_start").val(start.format('YYYY-MM-DD'));
        $("#car_date_finish").val(end.format('YYYY-MM-DD'));
    });
});

$("#show_result").click(function () {
    $.ajax({
        url: "/MapAJAX/ShowArchiveLog",
        method: "get",
        type: "GET",
        data: {
            terminal: $("#slct_car").val(),
            start: $("#car_date_start").val(),
            finish: $("#car_date_finish").val(),
            name: $("#slct_car option:selected").text()
        },
        beforeSend() {
            $("#loadingcarinfo").css("display", "block");
            $('#data_load_car_info').css("display", "none");
        },
        success: function (response) {
            $("#data_load_car_info").html("");
            $("#data_load_car_info").html(
                `<table class="table table-hover table-bordered">
                    <thead>
                        <tr class="text-center">
                            <th>Dövlət nişanı</th>
                            <th>Qət etdiyi məsafə</th>
                            <th>Ortalama sürət</th>
                            <th>Xəritədə göstər</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="text-center">
                            <td>${response.list[0].name}</td>
                            <td>${response.sumdistance} km</td>
                            <td>${response.avgspeed} km/saat</td>
                            <td><span class="far fa-play-circle play_car_animation" data-close="car-info-modal"></span></td>
                        </tr>
                    </tbody>
                 </table>`
            );
            //Empty array
            while (carlogdata.length) {
                carlogdata.pop();
            };

            //Load array
            for (var i = 0; i < response.list.length; i++) {
                carlogdata.push(response.list[i]);
            };
        },
        complete() {
            $("#loadingcarinfo").css("display", "none");
            $('#data_load_car_info').css("display", "block");
        }
    });
});
//-------------------------------------------------------------------------------------------------------------------