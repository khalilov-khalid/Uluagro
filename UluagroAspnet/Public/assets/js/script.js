//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Bildiris sistemi >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

//yeni bildirisin yoxlanmasi
var unreadercount = 0;
$(document).ready(function () {
    ALLMesagesRefresh();
    setInterval(function () {
        unreadercount = $("button.ReadMEssages[data-bool='True']").length
        $.ajax({
            url: "/AjaxsRequest/CheckNotificationCount",
            method: "post",
            success: function (response) {
                if (response != 0) {
                    $("#NotficationCount").text(response)
                    $("#NotficationCount").removeAttr("hidden")
                }
                else {
                    $("#NotficationCount").attr("hidden", "hidden")
                }
                if (response != unreadercount) {
                    ALLMesagesRefresh();
                }
            }
        })   
    }, 10000);
});

// sisteme giris eden sexsin butun mesajlari
function ALLMesagesRefresh() {
    $.ajax({
        url: "/AjaxsRequest/Notification",
        method: "post",
        success: function (response) {
            $("#NotficationMessages").html("");
            $("#NotficationMessages").append(response)
            unreadercount = $("button.ReadMEssages[data-bool='True']").length
            
        }
    })        
}
// is tamamlanandan sonra tamlamlandi buttonun  basdiqda is planlamasinin statusunun deyisir.
$(document).on("click", ".workplancolplate", function () {
    var messageid = $(this).attr("data-wp")
    $(this).removeClass(".workplancolplate");
    $(this).addClass("bg-tr-2");
    $.ajax({
        url: "/AjaxsRequest/WorkPlanComplated",
        data: { id: messageid},
        method: "post",
        success: function (response) {
            console.log($(this))
            unreadercount = $("button.ReadMEssages[data-bool='True']").length
            console.log(unreadercount)
        }
    }) 
})
// yeni mesagi oxuyandan sonra mesajin statusun deyisir,
$(document).on("click", ".ReadMEssages", function () {
    var messageid = $(this).attr("data-mesage");
    var messagestatus = $(this).attr("data-bool")
    if (messagestatus == "True") {        
        $.ajax({
            url: "/AjaxsRequest/Reading",
            data: { id:messageid },
            method: "post",
            type: "JSON",
            success: function (response) {
                console.log(response)               
            }           
        })
        $(this).attr("data-bool", "False");
        $(this).closest('.unread').removeAttr("style");
    }
})
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< bildiris sistemlerinin  bitmesi>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>



//The ToJavaScriptDate() function accepts a value in \/Date(ticks)\/ format and returns a date string in MM/dd/yyyy format.
//method 1.
function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    var MM = (dt.getMonth() + 1);
    var dd = dt.getDate();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (MM < 10) {
        MM = '0' + MM;
    }

    return MM + "/" + dd + "/" + dt.getFullYear();
}

// get today

function getToday() {
    var today = new Date();
    var dd = today.getDate();
    var MM = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (MM < 10) {
        MM = '0' + MM;
    }

    return MM + '/' + dd + '/' + yyyy
}
// method 2.
function convert(defaultDate) {
    var str = parseInt(defaultDate.slice(6, 19));
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    hours = ("0" + date.getHours()).slice(-2);
    minutes = ("0" + date.getMinutes()).slice(-2);
    return mnth + "/" + day + "/" + date.getFullYear() + " " + hours + ":" + minutes
}

//Xerite tarixcesi bolmesi
//Xerite tarixcesi bolmesinde filtirleme
var mapList = [
    { typename: "Color Infrared (Vegetation)", month: "Oktyabr", year: "2018", typenumber: "0", img: "645.jpg" },
    { typename: "ARVI", month: "Oktyabr", year: "2018", typenumber: "1", img: "arvi.png" },
    { typename: "Natural Color", month: "Oktyabr", year: "2018", typenumber: "2", img: "natural.jpg" },
    { typename: "Color Infrared (Vegetation)", month: "Oktyabr", year: "2018", typenumber: "3", img: "645.jpg" },
    { typename: "SAVI", month: "Oktyabr", year: "2018", typenumber: "4", img: "savi.png" },
    { typename: "Atmospheric Penetration", month: "Oktyabr", year: "2018", typenumber: "5", img: "atosfer.jpg" },
    { typename: "Azer Kosmos", month: "Oktyabr", year: "2018", typenumber: "6", img: "atosfer.jpg" }

]
for (var i = 0; i < mapList.length; i++) {
    $("#map-view-type").append("<option value='" + mapList[i].typename + "'>" + mapList[i].typename + "</option>")
    $(".map-details tbody").append("<tr><td class='text- center'>" + i + "</td><td class='text-center'><img src='/Public/assets/images/" + mapList[i].img + "' alt='Növü' width='40' height='40' class='radius-50'></td><td width='100%'>" + mapList[i].typename + "</td><td>" + mapList[i].month + "</td><td>" + mapList[i].year + "</td><td><button class='map-history btn xs circle bg-tr-2' data-nov='" + mapList[i].typenumber + "'><i aria-hidden='true' class='icon-eye'></i></button></td><tr>")
}
var searcharry = ["", "", ""];
function SearchResult() {
    $(".map-details tbody tr").remove();
    var list = mapList;
    if (searcharry[0] != "") {
        list = list.filter(type => type.typename == searcharry[0]);
    }
    if (searcharry[1] != "") {
        list = list.filter(type => type.month == searcharry[1]);
    }
    if (searcharry[2] != "") {
        list = list.filter(type => type.year == searcharry[2]);
    }
    for (var i = 0; i < list.length; i++) {
        $(".map-details tbody").append("<tr><td class='text- center'>" + i + "</td><td class='text-center'><img src='/Public/assets/images/" + mapList[i].img + "' alt='Növü' width='40' height='40' class='radius-50'></td><td width='100%'>" + list[i].typename + "</td><td>" + list[i].month + "</td><td>" + list[i].year + "</td><td><button class='map-history btn xs circle bg-tr-2' data-nov='" + list[i].typenumber + "'><i aria-hidden='true' class='icon-eye'></i></button></td><tr>")
    }
}
$("#map-view-type").on("change", function () {
    searcharry[0] = this.value;
    SearchResult()
})

$("#map-view-mounth").on("change", function () {
    searcharry[1] = this.value;
    SearchResult();
})

$("#map-view-year").on("change", function () {
    searcharry[2] = this.value;
    SearchResult();
})


//var winPrint = window.open('', '', 'left=0,top=0,width=800,height=600,toolbar=0,scrollbars=0,status=0');
//winPrint.document.write('<title>Print  Report</title><br /><br /> Hellow World');
//winPrint.document.close();
//winPrint.focus();
//winPrint.print();
//winPrint.close(); 


// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Financial Raport>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//get financial type data and inout data
var FinancialInout = [];
var FinancialType = [];
$("#_OpenFinancalReport").on("click", function () {
    $.ajax({
        url: "/AjaxsRequest/FinancialTypeInout",
        method: "post",
        type: "JSON",
        success: function (response) {
            AllFinancialReports(response._AllFinancialReport)
            FinancialInout = response._PaymentInout;
            FinancialType = response._PaymentType;
        }
    })
})


// Add new Financial line
$("#_addNewFinancial").on("click", function () {
    $("#_updateFinancialRaport").css("display", "none")
    $("#_createFinancialRaport").css("display", "inline")

    var today = new Date();
    var dd = today.getDate();
    var MM = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    var hh = today.getHours();
    var mm = today.getMinutes();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (MM < 10) {
        MM = '0' + MM;
    }

    today = MM + '/' + dd + '/' + yyyy + " " + hh + ":" + mm;
    
    $("#_raportdate").val(today);

    $("#_raportInout option").remove();
    $("#_raportInout").append("<option selected disabled value=''>Seç</option>")
    for (var i = 0; i < FinancialInout.length; i++) {
        $("#_raportInout").append("<option value='" + FinancialInout[i].OBJECTID + "'>" + FinancialInout[i].NAME + "</option>")
    }

    $("#_raportType option").remove();
    $("#_raportType").append("<option selected disabled value='' >Seç</option>")
    for (var i = 0; i < FinancialType.length; i++) {
        $("#_raportType").append("<option value='" + FinancialType[i].OBJECTID + "' >" + FinancialType[i].NAME + "</option>")
    }
})

// get total mainpayment and vat
function totalPayment(_mainpayment, _vat, _totalpayment) {
    var mainpay = $("#" + _mainpayment).val();
    var vat = $("#" + _vat).val();

    if (mainpay == '') {
        $("#" + _totalpayment).val(vat);
    }
    else if (vat == '') {
        $("#" + _totalpayment).val(mainpay);
    }
    else {
        var total = parseFloat(vat) + parseFloat(mainpay);
        $("#" + _totalpayment).val(total);
    }
    
}


// create new financial report    
$("#_FinancialRaport").on("submit", function (e) {     
    e.preventDefault();
    var checkedForm = $(this).attr("data-id")
    if (checkedForm=="") {
        var formdata = $(this).serializeArray();
        var data = {};
        $(formdata).each(function (index, obj) {
            data[obj.name] = obj.value;
        });
        $.ajax({
            url: "/AjaxsRequest/CreateFinancialRaport",
            data: { _payment: data },
            method: "post",
            type: "JSON",
            success: function (response) {
                if (response == false) {
                    console.log("test false modal")
                }
                else {
                    AllFinancialReports(response)
                    console.log("create")
                    //create
                    $("#_closeRaport").click();
                }
            }
        })
        document.getElementById('_FinancialRaport').reset();
    }
    else {
        var formdata = $(this).serializeArray();
        var data = {};
        $(formdata).each(function (index, obj) {
            data[obj.name] = obj.value;
        });
        $.ajax({
            url: "/AjaxsRequest/FinishUpdateReport",
            data: { _payment: data, _id: checkedForm },
            method: "post",
            type: "JSON",
            success: function (response) {
                AllFinancialReports(response)
                console.log("update")
                //update
                $("#_closeRaport").click();
            }
        })
        $("#_FinancialRaport").attr("data-id", "")
        document.getElementById('_FinancialRaport').reset();

    }
         
})

$("#_closeRaport").on("click", function () {
    $("#_FinancialRaport").attr("data-id", "")
    document.getElementById('_FinancialRaport').reset();
})

// get all Financial Reports and refresh.
function AllFinancialReports(_allReports) {
    $("#_AllFinancialReports tbody tr").remove();
    for (var i = 0; i < _allReports.length; i++) {
        var template = "<tr>"
                            +"<td>"+ (i+1) +"</td>"
                            +"<td>"+ convert(_allReports[i].DATE)+"</td>"
                            +"<td>"+_allReports[i].ID_INOUT +"</td>"
                            +"<td>"+_allReports[i].PAYMENT_TYPE_ID +"</td>"
                            +"<td>"+_allReports[i].MAIN_PAYMENT+" <i class='icon-currency-azn'></i></td>"
                            +"<td>"+_allReports[i].VAT+" <i class='icon-currency-azn'></td>"
                            +"<td>"+_allReports[i].ALL_PAYMENT+" <i class='icon-currency-azn'></i></td>"
                            +"<td>"+_allReports[i].REMNANT+" <i class='icon-currency-azn'></i></td>"
                            +"<td id='Financialbuttons"+i+"'>"
                                +"<button data-id='"+_allReports[i].OBJECTID+"' class='btn xs circle bg-blue' data-target-modal='detail-financial-report'>"
                                    +"<i aria-hidden='true' class='icon-info-circle-1'></i>"
                                +"</button>"                                
                            +"</td>"
                        +"</tr>"

        $("#_AllFinancialReports tbody").append(template);

        var editButton = "<button id='_UpdateReport' data-id='" + _allReports[i].OBJECTID + "' class='btn xs circle bg-orange' data-target-modal='create-financial-report'>"
                            + "<i aria-hidden='true' class='icon-pen-5'></i>"
                         + "</button>"

        console.log("part 1")

        console.log(ToJavaScriptDate(_allReports[i].DATE))
        console.log(getToday())
        if (ToJavaScriptDate(_allReports[i].DATE) == getToday()) {
            $("#Financialbuttons" + i).append(editButton)
        }
    }
}


// open update modal and get data update report
$(document).on("click", "#_UpdateReport", function () {
    var reportID = $(this).attr("data-id");

    $.ajax({
        url: "/AjaxsRequest/UpdateReport",
        data: { _payment: reportID },
        method: "post",
        type: "JSON",
        success: function (response) {
            var formInputs = $("#_FinancialRaport input");
            for (var i = 0; i < formInputs.length; i++) {
                if (i==0) {
                    var inputname = $(formInputs[i]).attr("name");
                    $(formInputs[i]).val(convert(response[inputname]))
                }
                else {
                    var inputname = $(formInputs[i]).attr("name");
                    $(formInputs[i]).val(response[inputname])
                }
            }
            var formselects = $("#_FinancialRaport select");
            for (var i = 0; i < formselects.length; i++) {
                var selectname = $(formselects[i]).attr("name");
                $(formselects[i]).val(response[selectname])
            }
            var textareaname = $("#_FinancialRaport textarea").attr("name");
            $("#_FinancialRaport textarea").val(response[textareaname])
            $("#_FinancialRaport").attr("data-id", response.OBJECTID)
        }
    })
})



//get info Financial Report




// Ajax request get list Work plan
var technicalCategorArray = [];
var maintechnicalArray = [];
var technicalArray = [];
var feltilizerCategoryArray = [];
var feltilizerArray = [];
var parcelArray = [];
var workarray = [];
var professionArray = [];
var workerArray = [];
$("#_ajxsWorkPlan").on("click", function () {
    $.ajax({
        url: "/AjaxsRequest/TechnicalCategoryList",
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log("ok")
            for (var i = 0; i < response._ParcelCategory.length; i++) {
                $("#_ParelCategory").append("<option value='" + response._ParcelCategory[i].OBJECTID + "'>" + response._ParcelCategory[i].NAME + "</option>")
            }
            parcelArray = response._Parcel;

            for (var i = 0; i < response._Responders.length; i++) {
                $("#_Responder").append("<option value='" + response._Responders[i].ID + "'>" + response._Responders[i].Name+"  "+ response._Responders[i].Surname+"</option>")
            }

            for (var i = 0; i < response._WorkCategory.length; i++) {
                $("#_WorkCategory").append("<option value='" + response._WorkCategory[i].OBJECTID + "'>" + response._WorkCategory[i].NAME + "</option>")
            }
            workarray = response._Work;
            professionArray = response._Profession;

            for (var i = 0; i < professionArray.length; i++) {
                $("#_WorkProfessions").append("<option value='" + professionArray[i].OBJECTID + "' >" + professionArray[i].NAME + "</option>")
            }

            workerArray = response._Worker;

            technicalCategorArray = response._TechniqueCategory;
            for (var i = 0; i < response._Technique.length; i++) {
                maintechnicalArray.push(response._Technique[i])
            }            
            technicalArray = JSON.parse(JSON.stringify(maintechnicalArray))
            feltilizerCategoryArray = response._FeltilizerCAtegory;
            feltilizerArray = response._Feltilizer
        }
    })
})


$("#_ParelCategory").on("change", function () {
    var id = this.value;
    $("#_Parcel").removeAttr("disabled")
    $("#_Parcel option").remove();
    $("#_Parcel").append("<option selected disabled value=''>Seç</option>")
    for (var i = 0; i < parcelArray.length; i++) {
        if (parcelArray[i].PARCELCATEGORYID==id) {
            $("#_Parcel").append("<option value='" + parcelArray[i].OBJECTID + "'>" + parcelArray[i].NAME + "</option>")
        }
    }
    $("#_ParselArea").val("");    
})

$("#_Parcel").on("change", function () {
    console.log(this.value)
    var foundIndex = parcelArray.findIndex(x => x.OBJECTID == this.value);
    var area = parcelArray[foundIndex].AREA;
    $("#_ParselArea").val(area);
})

$("#_WorkCategory").on("change", function () {
    var categoryID = this.value;
    $("#_Works option").remove();
    $("#_Works").removeAttr("disabled")
    $("#_Works").append("<option selected disabled value=''>Seç</option>")
    for (var i = 0; i < workarray.length; i++) {
        if (workarray[i].WORK_CAT_ID == categoryID) {
            $("#_Works").append("<option value='" + workarray[i].OBJECTID + "'>" + workarray[i].NAME + "</option>")
        }
    }
})

//get technical by Technical Category
function getTechical(value, id) {
    var _id = '#' + id
    $(_id + ' option').remove();
    $(_id).append("<option selected disabled value=''>Seç</option>")
    $(_id).removeAttr("disabled")
    for (var i = 0; i < technicalArray.length; i++) {
        if (technicalArray[i].CATEGORYID == value) {
            if (technicalArray[i].WORKINGSTATUS == 1) {
                $(_id).append("<option style='background:lightgreen' value='" + technicalArray[i].OBJECTID + "'>" + technicalArray[i].NAME + "</option>")
            }
            if (technicalArray[i].WORKINGSTATUS == 2) {
                $(_id).append("<option disabled style='background:yellow' value='" + technicalArray[i].OBJECTID + "'>" + technicalArray[i].NAME + "</option>")
            }
            if (technicalArray[i].WORKINGSTATUS == 3)
            {
                $(_id).append("<option disabled style='background:red' value='" + technicalArray[i].OBJECTID + "'>" + technicalArray[i].NAME + "</option>")
            }
        }
    }
}

//selected technicals
function selectTechnical(value) {
    technicalArray = [];
    technicalArray = JSON.parse(JSON.stringify(maintechnicalArray))   
    

    var allSelectedTechnical = $(".testvalue");
    for (var i = 0; i < allSelectedTechnical.length; i++) {
        var foundIndex = technicalArray.findIndex(x => x.OBJECTID == allSelectedTechnical[i].value);
        technicalArray[foundIndex].WORKINGSTATUS = 2;
    }

    for (var x = 0; x < allSelectedTechnical.length; x++) {
        var id = '#' + allSelectedTechnical[x].id;
        var value = $(id).val();
        var found = technicalArray.find(function (element) {
            if (element.OBJECTID == value) {
                var x = element.CATEGORYID
            }
            return x;
        });
        var xelement = found.CATEGORYID;
        $(id + ' option').remove();

        $(id).append("<option disabled value=''>Seç</option>")
        for (var i = 0; i < technicalArray.length; i++) {
            if (technicalArray[i].CATEGORYID == xelement) {                
                if (technicalArray[i].OBJECTID == value) {
                    $(id).append("<option selected value='" + technicalArray[i].OBJECTID + "'>" + technicalArray[i].NAME + "</option>")
                }
                if (technicalArray[i].WORKINGSTATUS == 1 && technicalArray[i].OBJECTID != value) {
                    $(id).append("<option style='background:lightgreen' value='" + technicalArray[i].OBJECTID + "'>" + technicalArray[i].NAME + "</option>")
                }
                if (technicalArray[i].WORKINGSTATUS == 2 && technicalArray[i].OBJECTID != value) {
                    $(id).append("<option disabled style='background:yellow' value='" + technicalArray[i].OBJECTID + "'>" + technicalArray[i].NAME + "</option>")
                }
                if (technicalArray[i].WORKINGSTATUS == 3 && technicalArray[i].OBJECTID != value) {
                    $(id).append("<option disabled style='background:red' value='" + technicalArray[i].OBJECTID + "'>" + technicalArray[i].NAME + "</option>")
                }
            }
        }        
    }
}

//get feltilizer by  Feltilizer category
function getFeltilizer(_catid,_id) {
    var id = '#' + _id
    $(id + ' option').remove();
    $(id).append("<option selected disabled value=''>Seç</option>")
    $(id).removeAttr("disabled")
    for (var i = 0; i < feltilizerArray.length; i++) {
        if (feltilizerArray[i].CATEGORYID == _catid) {
            $(id).append("<option  value='" + feltilizerArray[i].OBJECTID + "'>" + feltilizerArray[i].NAME + "</option>")
        }
    }
}

//Delete disable Water quantity in feltilizer
function enableWater(_id) {
    var id = '#' + _id;
    $(id).removeAttr("disabled");
}

//get Feltilizer quantity by Water quantity
function computationFeltilizerQuantity(_value,_id,_fletilizerid) {
    var id = '#' + _id;
    var feltilizerid = $('#' + _fletilizerid).val();
    var foundIndex = feltilizerArray.findIndex(x => x.OBJECTID == feltilizerid);
    var feltilizerquantity = feltilizerArray[foundIndex].WATERKG;
    $(id).removeAttr("disabled");
    var x = (feltilizerquantity / 100) * _value;
    $(id).val(x.toFixed(2));
}


//get worker by professions
var workerlineid = 0;
function getWorker(professionId, lineid) {
    var line = '#' + lineid;
    $(line + ' ul').remove();
    $(line).prev().removeAttr("disabled")
    $(line).append("<ul class='menu-body' role='menu'></ul>");
    for (var i = 0; i < workerArray.length; i++) {
        if (workerArray[i].PROFESSIONID == professionId) {
            if (workerArray[i].WorkingStatus==1) {
                $(line + ' ul').append("<li role='presentation'><span class='menu-item ckbox' role='menuitem'><input type='checkbox' name='workerID' data-id='" + workerArray[i].OBJECTID + "' id='worker-" + workerlineid + "'><label for='worker-" + workerlineid + "'>" + workerArray[i].FULLNAME + "</label></span></li>")
            }
            if (workerArray[i].WorkingStatus==3) {
                $(line + ' ul').append("<li role='presentation'><span class='menu-item ckbox' role='menuitem'><input disabled type='checkbox' data-id='" + workerArray[i].OBJECTID + "' id='worker-" + workerlineid + "'><label for='worker-" + workerlineid + "'>" + workerArray[i].FULLNAME + "</label></span></li>")
            }
        }
        workerlineid++;
    }

}


////<<<<<<<<<<<<<<<<<<< Work Plan Add New Dayly Plan>>>>>>>>>>>>>>>>>>>>
var dailyId = 0;
var technicalgroupID = 0;
var feltilizerGroupID = 0;
var professionLinecount = 0;
var workerlineid = 0;
var queuelinecount = 0;
// create queue work plan
function AddQueue(value, taskid) {
    
    var count = 1;
    var id = '#' + taskid

    $(id + ' div').remove();

    if (value == 0) {
        queuelinecount++
        var queuetemplate = "<div class='pt-10 pl-10 pr-10' id='queueline" + queuelinecount+"'>"
                                +"<div class='row as-5'>"                                    
                                    +"<div class='col as-6 xs-6 xxs-12 mb-10'>"
                                        +"<label for='_WorkProfessions'>Vəzifələr</label>"
                                        +"<select required class='input' onchange="+"getWorker(this.value,'Workers"+workerlineid+"')"+" id='_WorkProfessions"+professionLinecount+"'>"
                                            +"<option value=''>Seç</option>"
                                        +"</select>"
                                    +"</div>"
                                    +"<div class='col as-6 xs-6 xxs-12 mb-10'>"
                                        +"<label for='_Workers'>İşçilər</label>"
                                        +"<div class='drop w-100p'>"
                                            +"<span role='button' disabled class='input drop-toggle pointer caret'>İşciləri seç</span>"
                                            +"<div class='drop-menu static animated fadeInUp' id='Workers"+workerlineid+"'>"
                                                +"<ul class='menu-body' role='menu'>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-1'>"
                                                            +"<label for='worker-1'>İşçi adı 1</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-2'>"
                                                            +"<label for='worker-2'>İşçi adı 2</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-3'>"
                                                            +"<label for='worker-3'>İşçi adı 3</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                +"</ul>"
                                            +"</div>"
                                        +"</div>"
                                    +"</div>"
                                    +"<div class='col as-12 mb-10'>"
                                        +"<label for='_WorkPlanNote'>Qeyd</label>"
                                        +"<textarea required rows='3' cols='50' id='_WorkPlanNote' class='input no-resize' name='note' placeholder='Qeyd'></textarea>"
                                    +"</div>"
                                    +"<div class='col as-3 xs-6 xxs-6 mb-10'>"
                                        +"<label for=''>label</label>"
                                        +"<input type='number' class='input' id=''>"
                                    +"</div>"
                                +"</div>"
                                +"<div class='row as-10 bg-gray'>"
                                    +"<div class='col as-12 pt-10 pl-10 pr-10'>"
                                        +"<div class='panel pt-10 pl-10 pr-10 mb-10'>"
                                            +"<div id='technicalGroup"+technicalgroupID+"'>"
                                                
                                            +"</div>"
                                            +"<div class='clear pb-10'>"
                                                +"<button type='button' onclick="+"Addtechical('technicalGroup"+technicalgroupID+"')"+" class='btn xs bg-orange'>Texnika əlavə et</button>"
                                            +"</div>"
                                        +"</div>"
                                        +"<div class='panel pt-10 pl-10 pr-10 mb-10'>"
                                            +"<div id='feltilizergroup"+feltilizerGroupID+"'>"
                                                
                                            +"</div>"
                                            +"<div class='clear pb-10'>"
                                                +"<button type='button' onclick="+"AddFeltilizer('feltilizergroup"+feltilizerGroupID+"')"+" class='btn xs bg-orange'>Dərman / Gübrə əlavə et</button>"
                                            +"</div>"
                                        +"</div>"
                                    +"</div>"
                                +"</div>"
                            +"</div>"
        $(id).append(queuetemplate);
        var ProfessionlineId = '#_WorkProfessions' + professionLinecount;
        for (var i = 0; i < professionArray.length; i++) {            
            $(ProfessionlineId).append("<option value='" + professionArray[i].OBJECTID + "'>" + professionArray[i].NAME + "</option>")
        }
        workerlineid++;
        technicalgroupID++
        feltilizerGroupID++

    }
    else {
        for (var i = 0; i < value; i++) {
            queuelinecount++
         var queuetemplate = "<div class='pt-10 pl-10 pr-10' id='queueline" + queuelinecount+"'>"
                                +"<div>Növbə "+ count+"</div>"
                                +"<div class='row as-5'>"
                                    +"<div class='col as-3 xs-6 xxs-6 mb-10'>"
                                        +"<label for=''>Başlama saatı</label>"
                                        +"<input required type='datetime-local' id='' class='input' name='queuestartdate' placeholder='Başlama Tarixi'>"
                                    +"</div>"
                                    +"<div class='col as-3 xs-6 xxs-6 mb-10'>"
                                        +"<label for=''>Bitmə saatı</label>"
                                        +"<input required type='datetime-local' id='' class='input' name='queueenddate' placeholder='Bitmə Tarixi'>"
                                    +"</div>"
                                    +"<div class='col as-3 xs-6 xxs-12 mb-10'>"
                                        +"<label for='_WorkProfessions'>Vəzifələr</label>"
                                        +"<select required class='input' onchange="+"getWorker(this.value,'Workers"+workerlineid+"')"+" id='_WorkProfessions"+professionLinecount+"'>"
                                            +"<option value=''>Seç</option>"
                                        +"</select>"
                                    +"</div>"
                                    +"<div class='col as-3 xs-6 xxs-6 mb-10'>"
                                        +"<label for='_Workers'>İşçilər</label>"
                                        +"<div class='drop w-100p'>"
                                            +"<span role='button' disabled class='input drop-toggle pointer caret'>İşciləri seç</span>"
                                            +"<div class='drop-menu static animated fadeInUp' id='Workers"+workerlineid+"'>"
                                                +"<ul class='menu-body' role='menu'>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-1'>"
                                                            +"<label for='worker-1'>İşçi adı 1</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-2'>"
                                                            +"<label for='worker-2'>İşçi adı 2</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-3'>"
                                                            +"<label for='worker-3'>İşçi adı 3</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                +"</ul>"
                                            +"</div>"
                                        +"</div>"
                                    +"</div>"
                                    +"<div class='col as-12 mb-10'>"
                                        +"<label for='_WorkPlanNote'>Qeyd</label>"
                                        +"<textarea required rows='3' cols='50' id='_WorkPlanNote' class='input no-resize' name='note' placeholder='Qeyd'></textarea>"
                                    +"</div>"
                                    +"<div class='col as-3 xs-6 xxs-6 mb-10'>"
                                        +"<label for=''>label</label>"
                                        +"<input type='number' class='input' id=''>"
                                    +"</div>"
                                +"</div>"
                                +"<div class='row as-10 bg-gray'>"
                                    +"<div class='col as-12 pt-10 pl-10 pr-10'>"
                                        +"<div class='panel pt-10 pl-10 pr-10 mb-10'>"
                                            +"<div id='technicalGroup"+technicalgroupID+"'>"
                                                
                                            +"</div>"
                                            +"<div class='clear pb-10'>"
                                                +"<button type='button' onclick="+"Addtechical('technicalGroup"+technicalgroupID+"')"+" class='btn xs bg-orange'>Texnika əlavə et</button>"
                                            +"</div>"
                                        +"</div>"
                                        +"<div class='panel pt-10 pl-10 pr-10 mb-10'>"
                                            +"<div id='feltilizergroup"+feltilizerGroupID+"'>"
                                                
                                            +"</div>"
                                            +"<div class='clear pb-10'>"
                                                +"<button type='button' onclick="+"AddFeltilizer('feltilizergroup"+feltilizerGroupID+"')"+" class='btn xs bg-orange'>Dərman / Gübrə əlavə et</button>"
                                            +"</div>"
                                        +"</div>"
                                    +"</div>"
                                +"</div>"
                            +"</div>"
    
            $(id).append(queuetemplate);
            var ProfessionlineId = '#_WorkProfessions' + professionLinecount;
            for (var x = 0; x < professionArray.length; x++) {
                $(ProfessionlineId).append("<option value='" + professionArray[x].OBJECTID + "'>" + professionArray[x].NAME + "</option>")
            }
            count++;
            technicalgroupID++;
            feltilizerGroupID++;
            professionLinecount++;
            workerlineid++;
         }
    }
    
}

//// create dayly work plan HTML in click new task
var dailyline = 0;
$("#_addDailyPlan").on("click", function () {
    dailyId++;
    technicalgroupID++;
    feltilizerGroupID++;
    professionLinecount++;
    dailyline++;
    queuelinecount++;
    var dailytemplate = "<div class='panel mb-10' id='dailyline" + dailyline+"'>"
                        +"<div class='panel-header'>"
                            +"<span class='panel-title bold float-left'>Növbəli tapşırıq</span>"
                            +"<span class='text-red float-right p-3 pointer' aria-label='Sil'>"
                                +"<i aria-hidden='true' class='icon-trash mr-5'></i>"
                                +"Sil"
                            +"</span>"
                        +"</div>"
                        +"<div class='panel-header bg-gray pb-0'>"
                            +"<div class='row as-5'>"
                                +"<div class='col as-4 xs-6 xxs-6 mb-10'>"
                                    +"<label for='_StartDate'>Başlama Tarixi</label>"
                                    +"<input required type='datetime-local' id='_StartDate' class='input' name='startdate' placeholder='Başlama Tarixi'>"
                                +"</div>"
                                +"<div class='col as-4 xs-6 xxs-6 mb-10'>"
                                    +"<label for='_EndDate'>Bitmə Tarixi</label>"
                                    +"<input required type='datetime-local' id='_EndDate' class='input' name='enddate' placeholder='Bitmə Tarixi'>"
                                +"</div>"
                                +"<div class='col as-4 xs-12 xxs-12 mb-10'>"
                                    +"<label for='Queue'>Növbə</label>"
                                    +"<select required class='input _selectqueue' name='queueId' id='Queue' onchange="+"AddQueue(this.value,'_queueGroup"+dailyId+"')"+">"
                                        +"<option value='0'>Növbəsiz</option>"
                                        +"<option value='2'>2 Növbəli</option>"
                                        +"<option value='3'>3 Növbəli</option>"
                                    +"</select>"
                                +"</div>"
                            +"</div>"
                        +"</div>"
                        +"<div id='_queueGroup"+dailyId+"'>"
                            +"<div class='pt-10 pl-10 pr-10' id='queueline" + queuelinecount+"'>"
                                +"<div class='row as-5'>"                                    
                                    +"<div class='col as-6 xs-6 xxs-12 mb-10'>"
                                        +"<label for='_WorkProfessions'>Vəzifələr</label>"
                                        +"<select required class='input' onchange="+"getWorker(this.value,'Workers"+workerlineid+"')"+" id='_WorkProfessions"+professionLinecount+"'>"
                                            +"<option value=''>Seç</option>"
                                        +"</select>"
                                    +"</div>"
                                    +"<div class='col as-6 xs-6 xxs-12 mb-10'>"
                                        +"<label for='_Workers'>İşçilər</label>"
                                        +"<div class='drop w-100p'>"
                                            +"<span role='button' disabled class='input drop-toggle pointer caret'>İşciləri seç</span>"
                                            +"<div class='drop-menu static animated fadeInUp' id='Workers"+workerlineid+"'>"
                                                +"<ul class='menu-body' role='menu'>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-1'>"
                                                            +"<label for='worker-1'>İşçi adı 1</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-2'>"
                                                            +"<label for='worker-2'>İşçi adı 2</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                    +"<li role='presentation'>"
                                                        +"<span class='menu-item ckbox' role='menuitem'>"
                                                            +"<input type='checkbox' id='worker-3'>"
                                                            +"<label for='worker-3'>İşçi adı 3</label>"
                                                        +"</span>"
                                                    +"</li>"
                                                +"</ul>"
                                            +"</div>"
                                        +"</div>"
                                    +"</div>"
                                    +"<div class='col as-12 mb-10'>"
                                        +"<label for='_WorkPlanNote'>Qeyd</label>"
                                        +"<textarea required rows='3' cols='50' id='_WorkPlanNote' class='input no-resize' name='note' placeholder='Qeyd'></textarea>"
                                    +"</div>"
                                    +"<div class='col as-3 xs-6 xxs-6 mb-10'>"
                                        +"<label for=''>label</label>"
                                        +"<input type='number' class='input' id=''>"
                                    +"</div>"
                                +"</div>"
                                +"<div class='row as-10 bg-gray'>"
                                    +"<div class='col as-12 pt-10 pl-10 pr-10'>"
                                        +"<div class='panel pt-10 pl-10 pr-10 mb-10'>"
                                            +"<div id='technicalGroup"+technicalgroupID+"'></div>"                                             
                                            +"<div class='clear pb-10'>"
                                                +"<button type='button' onclick="+"Addtechical('technicalGroup"+technicalgroupID+"')"+" class='btn xs bg-orange'>Texnika əlavə et</button>"
                                            +"</div>"
                                        +"</div>"
                                        +"<div class='panel pt-10 pl-10 pr-10 mb-10'>"
                                            +"<div id='feltilizergroup"+feltilizerGroupID+"'></div>"
                                            +"<div class='clear pb-10'>"
                                                +"<button type='button' onclick="+"AddFeltilizer('feltilizergroup"+feltilizerGroupID+"')"+" class='btn xs bg-orange'>Dərman / Gübrə əlavə et</button>"
                                            +"</div>"
                                        +"</div>"
                                    +"</div>"
                                +"</div>"
                            +"</div>"
                        +"</div>"
                    +"</div>"

    $("#_dailyPlanGroup").append(dailytemplate);
    
    var ProfessionlineId = '#_WorkProfessions' + professionLinecount;
    for (var i = 0; i < professionArray.length; i++) {
        $(ProfessionlineId).append("<option value='" + professionArray[i].OBJECTID + "'>" + professionArray[i].NAME + "</option>")
    }
    workerlineid++;
    technicalgroupID++
    feltilizerGroupID++
})


//// Add new Technical line
var TechnicalLineID = 0;
var TechnicalCategory = 0;
var driverandtrailercount = 0;
function Addtechical(lineid) {
    var techicalLine="<div class='row as-5' id='tecnicalline"+TechnicalLineID+"'>"
                        +"<div class='col xs-6 xxs-6 mb-10'>"
                            +"<label for='_TechnicalType'>Texnikanın növü</label>"
                            +"<select required class='input' name='tecnicalcategory' onchange="+"getTechical(this.value,'_Technical"+TechnicalLineID+"')"+" id='_TechnicalType"+TechnicalLineID+"'>"
                                +"<option selected disabled value=''>Seç</option>"
                            +"</select>"
                        +"</div>"
                        +"<div class='col xs-6 xxs-6 mb-10'>"
                            +"<label for='_Technical'>Texnika</label>"
                            +"<select required class='input testvalue' name='tehcnical' onchange="+"selectTechnical(this.value)"+" id='_Technical"+TechnicalLineID+"' disabled>"
                                +"<option value=''>Seç</option>"
                            +"</select>"
                        +"</div>"
                        +"<div class='col xs-6 xxs-6 mb-10'>"
                            +"<label for='_DriverWorker'>Sürücü</label>"
                            +"<div class='drop w-100p'>"
                                +"<span role='button' class='input drop-toggle pointer caret'>Sürücüləri seç</span>"
                                +"<div class='drop-menu static animated fadeInUp' id='_DriverWorker"+TechnicalLineID+"' >"
                                    +"<ul class='menu-body' role='menu'>"
                                        +"<li role='presentation'>"
                                            +"<span class='menu-item' role='menuitem'>"
                                                +"<div class='ckbox'>"
                                                    +"<input type='checkbox' id='driver-1'>"
                                                    +"<label for='driver-1'>Sürücü adı 2</label>"
                                                +"</div>"
                                            +"</span>"
                                        +"</li>"
                                        +"<li role='presentation'>"
                                            +"<span class='menu-item' role='menuitem'>"
                                                +"<div class='ckbox'>"
                                                    +"<input disabled type='checkbox' id='driver-2'>"
                                                    +"<label style='color:red' for='driver-2'>Sürücü adı 2</label>"
                                                +"</div>"
                                            +"</span>"
                                        +"</li>"
                                        +"<li role='presentation'>"
                                            +"<span class='menu-item ckbox' role='menuitem'>"
                                                +"<div class='ckbox'>"
                                                    +"<input type='checkbox' id='driver-3'>"
                                                    +"<label for='driver-3'>Sürücü adı 2</label>"
                                                +"</div>"
                                            +"</span>"
                                        +"</li>"
                                    +"</ul>"
                                +"</div>"
                            +"</div>"
                        +"</div>"
                        +"<div class='col xs-6 xxs-6 mb-10'>"
                            +"<label for='_Trailers'>Qoşğu</label>"
                            +"<div class='drop w-100p'>"
                                +"<span role='button' class='input drop-toggle pointer caret'>Qoşğuları seç</span>"
                                +"<div class='drop-menu static animated fadeInUp' id='_Trailers"+TechnicalLineID+"'>"
                                    +"<ul class='menu-body' role='menu'>"
                                        +"<li role='presentation'>"
                                            +"<span class='menu-item ckbox' role='menuitem'>"
                                                +"<input type='checkbox' id='trailers-1'>"
                                                +"<label for='trailers-1'>Qoşğu adı 1</label>"
                                            +"</span>"
                                        +"</li>"
                                        +"<li role='presentation'>"
                                            +"<span class='menu-item ckbox' role='menuitem'>"
                                                +"<input type='checkbox' id='trailers-2'>"
                                                +"<label for='trailers-2'>Qoşğu adı 2</label>"
                                            +"</span>"
                                        +"</li>"
                                        +"<li role='presentation'>"
                                            +"<span class='menu-item ckbox' role='menuitem'>"
                                                +"<input type='checkbox' id='trailers-3'>"
                                                +"<label for='trailers-3'>Qoşğu adı 3</label>"
                                            +"</span>"
                                        +"</li>"
                                    +"</ul>"
                                +"</div>"
                            +"</div>"
                        +"</div>"
                        +"<div class='col as-1 xs-3 xxs-3 mb-10'>"
                            +"<label class='hide-xs hide-xxs'> </label>"
                            +"<button type='button' onclick="+"DeleteLine('tecnicalline"+TechnicalLineID+"')"+" class='btn bg-white w-100p' aria-label='Sil'>"
                                +"<i aria-hidden='true' class='icon-minus'></i>"
                            +"</button>"
                        +"</div>"
                    +"</div>"

    var id = '#' + lineid;
    $(id).append(techicalLine);  
    
    var cat = '#_TechnicalType' + TechnicalLineID;
    for (var i = 0; i < technicalCategorArray.length; i++) {
        $(cat).append("<option value=" + technicalCategorArray[i].OBJECTID +">" + technicalCategorArray[i].NAME+"</option>")
    }

    var driver = '#_DriverWorker' + TechnicalLineID;
    $(driver + ' ul').remove();
    $(driver).append("<ul class='menu-body' role='menu'></ul>")
    for (var i = 0; i < workerArray.length; i++) {
        if (workerArray[i].PROFESSIONID == 8) {
            $(driver + ' ul').append("<li role='presentation'><span class='menu-item ckbox' role='menuitem'><input type='checkbox' name='driverid' data-id='" + workerArray[i].OBJECTID + "' id='driver-" + driverandtrailercount + "'><label for='driver-" + driverandtrailercount + "'>" + workerArray[i].FULLNAME + "</label></span></li>");
        }
        driverandtrailercount++;
    }
    var trailer = '#_Trailers' + TechnicalLineID;
    $(trailer + ' ul').remove();
    $(trailer).append("<ul class='menu-body' role='menu'></ul>")
    for (var i = 0; i < technicalArray.length; i++) {
        if (technicalArray[i].CATEGORYID == 4) {
            $(trailer + ' ul').append("<li role='presentation'><span class='menu-item ckbox' role='menuitem'><input type='checkbox' name='trailerid' data-id='" + technicalArray[i].OBJECTID + "' id='trailers-" + driverandtrailercount + "'><label for='trailers-" + driverandtrailercount + "'>" + technicalArray[i].NAME + "</label></span></li>");
        }
        driverandtrailercount++;
    }
    TechnicalLineID++
}

// Add New Feltilizer Line;
var FeltilizerLineID = 0;
function AddFeltilizer(FeltilizerID) {
    var feltilizerLine="<div class='row as-5' id='FeltilizerLine"+FeltilizerLineID+"'>"
                            +"<div class='col xs-6 xxs-6 mb-10'>"
                                +"<label for='_FertilizerCategory'>Kateqoriya</label>"
                                +"<select required class='input' name='feltilizercategoryid' onchange="+"getFeltilizer(this.value,'_Fertilizer"+FeltilizerLineID+"')"+" id='_FertilizerCategory"+FeltilizerLineID+"'>"
                                    +"<option value=''>Seç</option>"
                                +"</select>"
                            +"</div>"
                            +"<div class='col xs-6 xxs-6 mb-10'>"
                                +"<label for='_Fertilizer'>Adı</label>"
                                +"<select required class='input' name='feltilizerid' onchange="+"enableWater('_Watherquantity"+FeltilizerLineID+"')"+" id='_Fertilizer"+FeltilizerLineID+"' disabled>"
                                    +"<option value=''>Seç</option>"
                                +"</select>"
                            +"</div>"
                            +"<div class='col xs-6 xxs-6 mb-10'>"
                                +"<label for='_Watherquantity'>Suyun Miqdarı</label>"
                                +"<input class='input' type='number' name='watherquantity' oninput="+"computationFeltilizerQuantity(this.value,'_Fertilizerquantity"+FeltilizerLineID+"','_Fertilizer"+FeltilizerLineID+"')"+" id='_Watherquantity"+FeltilizerLineID+"' value='' disabled />"
                            +"</div>"
                            +"<div class='col xs-6 xxs-6 mb-10'>"
                                +"<label for='_Fertilizerquantity'>Miqdarı</label>"
                                +"<input class='input' type='number' name='feltilizerquantity' id='_Fertilizerquantity"+FeltilizerLineID+"' value='' disabled />"
                            +"</div>"
                            +"<div class='col as-1 xs-3 xxs-3 mb-10'>"
                                +"<label class='hide-xs hide-xxs'> </label>"
                                +"<button type='button' onclick="+"DeleteLine('FeltilizerLine"+FeltilizerLineID+"')"+" class='btn bg-white w-100p' aria-label='Sil'>"
                                    +"<i aria-hidden='true' class='icon-minus'></i>"
                                +"</button>"
                            +"</div>"
                        +"</div>"

    var Id = '#' + FeltilizerID
    $(Id).append(feltilizerLine);

    var cat = '#_FertilizerCategory' + FeltilizerLineID;
    for (var i = 0; i < feltilizerCategoryArray.length; i++) {
        $(cat).append("<option value=" + feltilizerCategoryArray[i].OBJECTID + ">" + feltilizerCategoryArray[i].NAME + "</option>")
    }   
    FeltilizerLineID++
}


//Delete Tecnikal line and Feltilizer Line
function DeleteLine(deletedLineID) {
    var id = '#' + deletedLineID;
    $(id).remove();
    
}


$("#WorkPlanForm").submit(function (e) {
    e.preventDefault();
    console.log("start")
    var mainWorkPlan = {
        mainstartdate: '',
        mainenddate: '',
        parcelId: 0,
        responderTd: 0,
        WorkId: 0,
        dailyWorkPlanArray:[]
    };

    var dailyPlan = {
        startdate:'',
        enddate:'',
        queueId:0,
        queueArray:[]
    }
    var queuePlan = {
        queuestartdate:'',
        queueenddate:'',
        workerID:[],
        note:'',
        waterquantity:'',
        technialArray:[],
        feltilizerArray:[]
    }
    var technicallist = {
        tecnicalcategory:0,
        tehcnical:0,
        driverid:[],
        trailerid:[]
    }
    var feltilizerlist = {
        feltilizercategoryid:0,
        feltilizerid:0,
        watherquantity:'',
        feltilizerquantity:'',
    }

    
    mainWorkPlan.mainstartdate = $('input[name=mainstartdate]').val();
    mainWorkPlan.mainenddate = $('input[name=mainenddate]').val();
    mainWorkPlan.parcelId = $('select[name=parcelId]').val();
    mainWorkPlan.responderTd = $('select[name=responderTd]').val();
    mainWorkPlan.WorkId = $('select[name=WorkId]').val();
    
    for (var i = 0; i < $("#_dailyPlanGroup").children().length; i++) {
        let dailyPlanCopy = JSON.parse(JSON.stringify(dailyPlan));
        var dailylineid = '#' + $("#_dailyPlanGroup").children()[i].id;
        dailyPlanCopy.startdate = $(dailylineid + ' input[name=startdate]').val();
        dailyPlanCopy.enddate = $(dailylineid + ' input[name=enddate]').val();
        dailyPlanCopy.queueId = $(dailylineid + ' select[name=queueId]').val();

        
        for (var x = 0; x < $(dailylineid + ' div[id^=_queueGroup]').children().length; x++) {
            let queuePlanCopy = JSON.parse(JSON.stringify(queuePlan));
            var queuelineid = '#' + $(dailylineid + ' div[id^=_queueGroup]').children()[x].id;
            if (dailyPlanCopy.queueId==0) {
                queuePlanCopy.queuestartdate = '';
                queuePlanCopy.queueenddate = '';
            }
            else {
                queuePlanCopy.queuestartdate = $(queuelineid + ' input[name=queuestartdate]').val();
                queuePlanCopy.queueenddate = $(queuelineid + ' input[name=queueenddate]').val();
            }
            var workers = $(queuelineid + ' input[name^=workerID]')
            var checedWorkercount = $(queuelineid + ' input[name^=workerID]:checked').length;
            console.log(checedWorkercount)
            if (!checedWorkercount) {
                alert("You must check at least one checkbox.");
                return false;
            }
            console.log(" isciler alindi alindi")
            for (var y = 0; y < workers.length; y++) {
                if ($(workers[y]).is(":checked")) {
                    queuePlanCopy.workerID.push(workers[y].getAttribute("data-id"))
                }
            }
            queuePlanCopy.note = $(queuelineid + ' textarea[name=note]').val();

            
            for (var q = 0; q < $(queuelineid + ' div[id^=technicalGroup]').children().length; q++) {
                let technicallistCopy = JSON.parse(JSON.stringify(technicallist));
                var technicallineid = '#' + $(queuelineid + ' div[id^=technicalGroup]').children()[q].id
                technicallistCopy.tecnicalcategory = $(queuelineid + ' select[name=tecnicalcategory]').val();
                technicallistCopy.tehcnical = $(queuelineid + ' select[name=tehcnical]').val();
                var drivers = $(queuelineid + ' input[name=driverid]')
                var checeddrivercount = $(queuelineid + ' input[name=driverid]:checked').length;
                console.log(checeddrivercount)
                if (!checeddrivercount) {
                    alert("You must check at least one checkbox.");
                    return false;
                }
                console.log(" suruculer alindi alindi")
                for (var a = 0; a < drivers.length; a++) {
                    if ($(drivers[a]).is(":checked")) {
                        technicallistCopy.driverid.push(drivers[a].getAttribute("data-id"))
                    }
                }
                var trailers = $(queuelineid + ' input[name=trailerid]')
                var checedtrailerscount = $(queuelineid + ' input[name=trailerid]:checked').length;
                console.log(checedtrailerscount)
                if (!checedtrailerscount) {
                    alert("You must check at least one checkbox.");
                    return false;
                }
                console.log(" qosqular alindi alindi")
                for (var t = 0; t < trailers.length; t++) {
                    if ($(trailers[t]).is(":checked")) {
                        console.log(trailers[t])
                        technicallistCopy.trailerid.push(trailers[t].getAttribute("data-id"))
                    }
                }
                queuePlanCopy.technialArray.push(technicallistCopy);
            }
            
            for (var v = 0; v < $(queuelineid + ' div[id^=feltilizergroup]').children().length; v++) {
                let feltilizerlistCopy = JSON.parse(JSON.stringify(feltilizerlist));
                var feltilizerlineid = '#' + $(queuelineid + ' div[id^=feltilizergroup]').children()[v].id;
                feltilizerlistCopy.feltilizercategoryid = $(queuelineid + ' select[name=feltilizercategoryid]').val();
                feltilizerlistCopy.feltilizerid = $(queuelineid + ' select[name=feltilizerid]').val();
                feltilizerlistCopy.watherquantity = $(queuelineid + ' input[name=watherquantity]').val();
                feltilizerlistCopy.feltilizerquantity = $(queuelineid + ' input[name=feltilizerquantity]').val();
                                
                queuePlanCopy.feltilizerArray.push(feltilizerlistCopy)
            }
            
            dailyPlanCopy.queueArray.push(queuePlanCopy);
        }
        mainWorkPlan.dailyWorkPlanArray.push(dailyPlanCopy)
    }

    $.ajax({
        url: "/AjaxsRequest/CreateWorkPlan",
        data: { workplan: mainWorkPlan },
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response)
        }
    })

    $("#_closeWorkPlan").click();
})



$("._WorkplanWatch").on("click", function () {
    ShowWorkPlanDetails(this.id);    
})


function ShowWorkPlanDetails(_id) {


}




//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Crops and Gardens>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>






//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<End Crops and Garden>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>








//<<<<<<<<<<<<<<<<<<<<<<objectler bolmesi>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
$("._depo").on("click", function () {
    $.ajax({
        url: "/AjaxsRequest/Depos",
        method: "post",
        type: "JSON",
        success: function (response) {

            if (response != null) {
                $("#depoList div").remove();
                for (var i = 0; i < response.length; i++) {
                    var strings = "<div class='col as-6 p-10 pb-0'>"
                                    + "<a href='#' class='d-block relative _onedepo' data-toggle='tooltip' data-depoNumber='" + response[i].OBJECTID + "' title='" + response[i].NAME + "' data-target-modal='depo-1'>"
                                    +"<i aria-hidden='true' class='icon-warehouse'></i>"
                                    +"</a>"
                                +"</div>"


                    $("#depoList").append(strings)
                }
            }
        }
    })
})

$(document).on("click", "._onedepo", function () {
    var _id = this.getAttribute("data-depoNumber");
    var name = this.getAttribute("title")
    $.ajax({
        url: "/AjaxsRequest/Depodetails",
        data: { id: _id },
        method: "post",
        type: "JSON",
        success: function (response) {
            $("#_depoContent table tbody tr").remove();
            $("#_depoName h4").remove();
            $("#_depoName").append("<h4 class='panel-title' >" + name + "</h4>")
            if (response) {
                for (var i = 0; i < response.length; i++) {
                    $("#_depoContent table tbody").append("<tr> <td class='text-center pl-5'>" + (i + 1) + "</td> <td>" + response[i].NAME + " ( " + response[i].TYPE + " )</td> <td>" + response[i].QUANTITY + "</td> </tr>")
                }
            }
        }
    })
})



//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< New Work Plan >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//yeni is planlamasinin codlari

var WorkPlanTechnichalsArray = [];//is planlamasinda tasklarin technicalari ozunda saxlayan array
var WorkPlanFeltilizersArray = []; //i planlamasinda tasklarin derman ve ya gubrelerini ozunde saxlayir

//yeni bir is planlamasi yaradilmasi ve is planlamasi ucun olan esas datalarin gelsesi ve htmlde yerlesmesi
$("#_WorkPlanCreateAndUpdate").on("click", function () {
    $.ajax({
        url: "/AjaxsRequest/GetAllDataWichWorkPlan",
        method: "post",
        type: "JSON",
        success: function (response) {
            $("#_workPlan").html("");
            $("#_workPlan").append(response)
        }
    })    
})

//Is planlamasinda saheye aid olub olmadigini secmek ucun checkboksa click olunanda saheye aid olan inputlari gizledir ve gosterir ve name atributun silib elave edir
$(document).on("click", "#_ParcelCheckBoxs", function () {
    if ($('#_ParcelCheckBoxs').is(":checked")) {
        $("#_ParcelsCategory").css("display", "block");
        $("#_ParcelsCategory select").attr("name", "parselCategortid");
        $("#_Parcels").css("display", "block");
        $("#_Parcels select").attr("name", "parselid");
    }
    else {
        $("#_ParcelsCategory").css("display", "none")
        $("#_ParcelsCategory select").removeAttr("name")
        $("#_Parcels").css("display", "none")
        $("#_Parcels select").removeAttr("name")

    }
})

//Is planlamasinda Islerin categoriyasina gore gorulecek isin getirilmesi
$(document).on("change", "#_WorkCategoryChange", function () {
    var dataid = $(this).val();
    $.ajax({
        url: "/AjaxsRequest/WorkCategoryChange",
        data: { id: dataid},
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response)
            $("#_WorkToBeDone option").remove();
            $("#_WorkToBeDone").append("<option selected disabled >Seç</option>")
            for (var i = 0; i < response.length; i++) {
                $("#_WorkToBeDone").append("<option value='" + response[i].OBJECTID + "' >'" + response[i].NAME + "'</option>")
            }
        }
    })
})

//sahenin categoriyasina gore sahelerin adlarinin getirir.
$(document).on("change", "#_parcelCategoryChange", function () {
    var dataid = $(this).val();
    $.ajax({
        url: "/AjaxsRequest/ParselCategoryChange",
        data: { id: dataid },
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response)
            $("#_parcelbyCategory option").remove();
            $("#_parcelbyCategory").append("<option selected disabled >Seç</option>")
            for (var i = 0; i < response.length; i++) {
                $("#_parcelbyCategory").append("<option value='" + response[i].OBJECTID + "' >" + response[i].NAME + "</option>")
            }
        }
    })
})

//is planlamasinda yeni task elave edir
$(document).on("click", "#_WorkPlanNewTask", function () {
    WorkPlanTechnichalsArray = [];//her yeni tapsiriqda tasklarin techihalar kistini saxlayan array sifirlanir
    WorkPlanFeltilizersArray = [];// her yeni tapsiriqda tasklarin derman ve ya gubre listini saxlayan array sifirlanir.
    $.ajax({
        url: "/AjaxsRequest/WorkPlanAddedNewTask",
        method: "post",
        type: "JSON",
        success: function (response) {
            $("#_workPlanTask").html("");
            $("#_workPlanTask").append(response)
        }
    })
})

//her defe yeni derman ve ya gubre elave etmek ucun line elave edir.
$(document).on("click", "#_AddWorkPlanTaskFeltilizerLine", function () {
    $.ajax({
        url: "/AjaxsRequest/WorkPlanAddedFeltilizerLine",
        method: "post",
        type: "JSON",
        success: function (response) {
            $("#_workPlanTaskAllFeltilizer").html("");
            $("#_workPlanTaskAllFeltilizer").append(response)
        }
    })
    $("#_AddWorkPlanTaskFeltilizerLine").attr("disabled", "true");
})

// derman ve ya gubreni elave etmek istemedikde line silir
$(document).on("click", "#_RemoveWorkPlanTaskFeltilizerLine", function () {
    console.log("salam")
    $("#_workPlanTaskAllFeltilizer").html("");
    $("#_AddWorkPlanTaskFeltilizerLine").removeAttr("disabled");

})

//derman ve ya gubreni yadda saxlayir/
$(document).on("click", "#_SaveWorkPlanTaskFeltilizer", function () {
    var workPlanNewFeltilizer = {

        feltilizercategory: { id: $("[name='Feltilzercategory']").val(), name: $("[name='Feltilzercategory'] option:selected").text() },
        feltilizer: { id: $("[name='Feltilizers']").val(), name: $("[name='Feltilizers'] option:selected").text() },
        watercount: $("[name='Watercount']").val(),
        Feltilizercount: $("[name='FeltilizerCount']").val()
    }
    WorkPlanFeltilizersArray.push(workPlanNewFeltilizer)
    $("#_workPlanTaskAllFeltilizer").html("");
    $("#_AddWorkPlanTaskFeltilizerLine").removeAttr("disabled");
    FeltilizerTable();
})

//derman ve ya gubre categoriyasina gore mehsulun adinin getirir.
$(document).on("change", "#_FeltilizrerCategoryChange", function () {
    var categoryid = $(this).val();
    $.ajax({
        url: "/AjaxsRequest/WorkPlanFeltilizerByCategory",
        data: { id: categoryid},
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response)
            $("#_feltilizerList option").remove();
            $("#_feltilizerList").append("<option selected disabled>Seç</option>")
            for (var i = 0; i < response.length; i++) {
                $("#_feltilizerList").append("<option value='" + response[i].OBJECTID + "' >" + response[i].NAME + "</option>")
            }
        }
    })
})

//is planlamasinin novbeli rejimine gore html deyismek
$(document).on("change", "#_WorkPLanTaskQueueChange", function () {
    var QueueCount = $(this).val();
    $.ajax({
        url: "/AjaxsRequest/WorkPlanTaskQueue",
        data: { queue: QueueCount},
        method: "post",
        type: "JSON",
        success: function (response) {
            $("#_WorkPlanTaskQueuePlace").html("");
            $("#_WorkPlanTaskQueuePlace").append(response)
        }
    })
})

//is planlamanin tapsiriq bolmesinde her defe yeni texnika line acir
$(document).on("click", '[id^="_AddWorkPlanTaskTechnichalLine_"]', function () {
    var datacount = $(this).attr("data-count")
    $.ajax({
        url: "/AjaxsRequest/WorkPlanAddedTaskNewTechnicalLine",
        method: "post",
        type: "JSON",
        success: function (response) {
            var appendenid = "#_workPlanTaskAllTechnichals_" + datacount;
            $(appendenid).html("");
            $(appendenid).append(response);
            var buttonid = "#_AddWorkPlanTaskTechnichalLine_" + datacount;
            $(buttonid).attr("disabled", "true");
        }
    })
})


//texnikalarin adini categoruyasina gore getirir.
$(document).on("change", ".TechnicalCategoryChange", function () {
    var catid = $(this).val();
    var lineid = $(this).closest('[id^="_workPlanTaskAllTechnichals_"]').attr("data-count")//seciled texnikanin hansi novbeye aid oldugunu secmek ucun.
    var appended = "#_workPlanTaskAllTechnichals_" + lineid + " .Technicals";//ve secilen novbeye gore texnikalarin adi olan hisseye append edir.
    $.ajax({
        url: "/AjaxsRequest/WorkPlanTaskQueueTecnical",
        data: { id: catid },
        method: "post",
        type: "JSON",
        success: function (response) {
            $(appended).html("");//append etmemisden evvel html temizleyir.
            $(appended).append("<option selected disabled>Seç</option>")
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $(appended).append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }           
        }
    })
})

$(document).on("change", ".TechnicalTrailerCategoryChange", function () {
    var catid = $(this).val();
    var lineid = $(this).closest('[id^="_workPlanTaskAllTechnichals_"]').attr("data-count")//seciled texnikanin hansi novbeye aid oldugunu secmek ucun.
    var appended = "#_workPlanTaskAllTechnichals_" + lineid + " .TechnicalTrailer";//ve secilen novbeye gore texnikalarin adi olan hisseye append edir.
    $.ajax({
        url: "/AjaxsRequest/WorkPlanTaskQueueTecnical",
        data: { id: catid },
        method: "post",
        type: "JSON",
        success: function (response) {
            $(appended).html("");//append etmemisden evvel html temizleyir.
            $(appended).append("<option selected disabled>Seç</option>")
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $(appended).append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }


        }
    })

})

//is planlamasinda eger texnika elave etmek istemedikde yandaki silmek duymesinine basdiqda html temizleyir
$(document).on("click", "._RemoveWorkPlanTaskTechnichalLine", function () {
    var lineid = $(this).closest('[id^="_workPlanTaskAllTechnichals_"]').attr("data-count")
    var parentid = "#_workPlanTaskAllTechnichals_" + lineid;
    $(parentid).html("");
    var buttonid = "#_AddWorkPlanTaskTechnichalLine_" + lineid;
    $(buttonid).removeAttr("disabled");
})

//texnikalari yadda saxla duymesine basdiqda datalari object halina salaraq qlobal arrayin icine atir. he line temizleyir.
$(document).on("click", "._SaveWorkPlanTaskTechnical", function () {
    var lineid = $(this).closest('[id^="_workPlanTaskAllTechnichals_"]').attr("data-count")//datanin hansi novbeye aid oldugunu gosterir.
    var data = "#_workPlanTaskAllTechnichals_" + lineid;//ve secilen novbeye gore texnikalarin adi olan hisseye append edir.
    var workPlanTechnical = {
        queueid:lineid,
        technicalcategory: { id: $(data + " [name='TechnicalCategory']").val(), name: $(data + " [name='TechnicalCategory'] option:selected").text()},
        technicalid: { id: $(data + " [name='Technical']").val(), name: $(data + " [name='Technical'] option:selected").text() },
        workerid: { id: $(data + " [name='Driver']").val(), name: $(data + " [name='Driver'] option:selected").text() },
        trailercategory: { id: $(data + " [name='TrailerCategory']").val(), name: $(data + " [name='TrailerCategory'] option:selected").text() },
        trailerid: { id: $(data + " [name='Trailer']").val(), name: $(data + " [name='Trailer'] option:selected").text() }
    }
    WorkPlanTechnichalsArray.push(workPlanTechnical);
    TecnichalTable(lineid)
    var parentid = "#_workPlanTaskAllTechnichals_" + lineid;
    $(parentid).html("");
    var buttonid = "#_AddWorkPlanTaskTechnichalLine_" + lineid;
    $(buttonid).removeAttr("disabled");
    
});


var WorkPlanTasksArray = [];//is planlamasinin tasklarinin oxunde saxlayan array;


//is planlamasinin icerisinde her yeni yaranan tasklari yadda saxlamaq ucundur.
$(document).on("click", "#_SaveWorkPlanTask", function () {
    //tasklarin umumi content-ini saxlayan object
    var WorkPlanTaks = {
        TaskStartDate: $("[name='StartDateTask']").val(),
        TaskEndDate: $("[name='EnddateTask']").val(),
        Queuecount: $("[name='QueueCount']").val(),
        QueueContent: [],
        TaskFetilizer: WorkPlanFeltilizersArray// tasklarin icerisinde olan derman ve gubrelerin saxlanildigi array
    }

    if (WorkPlanTaks.Queuecount == 1) {

        var WorkPlanTaskQueue = {
            QueueStartHours:"",
            QueueEndHours:"",
            QueueProfessin: $("#WorkPlanQueueForm_0 [name='QueueProfessin']").val(),
            QueueWorker: $("#WorkPlanQueueForm_0 [name='QueueName']").val(),
            QueueNote: $("#WorkPlanQueueForm_0 [name='Queuenote']").val(),
            QueueTechnicallist: []//her tasklarin oz tecnikalarini elave etmek ucun array.
        }
        WorkPlanTaskQueue.QueueTechnicallist.push(WorkPlanTechnichalsArray);//texnikalar elave elinen yer

        WorkPlanTaks.QueueContent.push(WorkPlanTaskQueue)//taskin novbe hissesinin objecti task contente elave edilir
        console.log(WorkPlanTaskQueue)
    }
    else {
        var test = $("form[id^='WorkPlanQueueForm_']");

        for (var i = 0; i < test.length; i++) {

            var formid = $(test[i]).attr("id");
            var QueueNumber = formid[formid.length - 1];
            var WorkPlanTaskQueue = {
                QueueStartHours: $("#" + formid + " [name='QueueStartDate']").val(),
                QueueEndHours: $("#" + formid + " [name='QueueEnddate']").val(),
                QueueProfessin: $("#" + formid + " [name='QueueProfessin']").val(),
                QueueWorker: $("#" + formid + " [name='QueueName']").val(),
                QueueNote: $("#" + formid + " [name='Queuenote']").val(),
                QueueTechnicallist: []//her tasklarin oz tecnikalarini elave etmek ucun array.
            }

            WorkPlanTaskQueue.QueueTechnicallist.push(WorkPlanTechnichalsArray.filter(word => word.queueid == QueueNumber));//texnikalar elave elinen yer

            WorkPlanTaks.QueueContent.push(WorkPlanTaskQueue)//taskin novbe hissesinin objecti task contente elave edilir
        }

    }

    WorkPlanTasksArray.push(WorkPlanTaks);
    console.log(WorkPlanTasksArray)
    
       
})



//,,,,,,,,,,, New Work Plan functions,,,,,,,,

//tasklarin icerisinde table-ye elave edilmis texnikalari yazir
function TecnichalTable(number) {
    var tableid = "#_WorkPlanTechnicalTable_" + number;
    var tablebody = tableid + " tbody";
    if (WorkPlanTechnichalsArray.length != 0) {
        $(tablebody).html("");
        for (var i = 0; i < WorkPlanTechnichalsArray.length; i++) {
            if (WorkPlanTechnichalsArray[i].queueid == number) {
                $(tablebody).append("<tr> <td>" + WorkPlanTechnichalsArray[i].technicalcategory.name + "</td> <td>" + WorkPlanTechnichalsArray[i].technicalid.name + "</td>  <td>" + WorkPlanTechnichalsArray[i].workerid.name + "</td> <td>" + WorkPlanTechnichalsArray[i].trailercategory.name + "</td>  <td>" + WorkPlanTechnichalsArray[i].trailerid.name + "</td>  <td>  <button type='button' data-index='" + i + "' class='btn xs circle bg-red _technicalDelete'> <i aria-hidden='true' class='icon-trash'></i></button></td>  </tr>")
            }
        }
        $(tableid).css("display", "block")        
    }
    else {
        $(tableid).css("display", "none")
        $(tablebody).html("");
    }
}

//tasklarin iceriinde table-ye elva edilmur derman ve ya gubreleri yazir
function FeltilizerTable() {
    if (WorkPlanFeltilizersArray.length != 0) {
        $("#_WorkPlanTaskFeltilizers tbody").html("");
        for (var i = 0; i < WorkPlanFeltilizersArray.length; i++) {
            $("#_WorkPlanTaskFeltilizers tbody").append("<tr> <td>" + WorkPlanFeltilizersArray[i].feltilizercategory.name + "</td> <td>" + WorkPlanFeltilizersArray[i].feltilizer.name + "</td>  <td>" + WorkPlanFeltilizersArray[i].watercount + "</td>  <td>" + WorkPlanFeltilizersArray[i].Feltilizercount + "</td>  <td>  <button type='button' data-index='" + i + "' class='btn xs circle bg-red _FeltilizerDelete'> <i aria-hidden='true' class='icon-trash'></i></button></td>  </tr>")
        }
        $("#_WorkPlanTaskFeltilizers").css("display", "block") 
    }
    else {
        $("#_WorkPlanTaskFeltilizers").css("display", "none")
        $("#_WorkPlanTaskFeltilizers tbody").html("");
    }
}






//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< End New Work Plan >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Crop Planning >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//Load Categories
$(document).ready(function () {
    $.ajax({
        url: "/AjaxsRequest/LoadParcelCategories",
        method: "get",
        //data: { id: $(this).val() },
        type: "JSON",
        success: function (response) {
            $("#category option").remove();
            $("#category").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#category").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }
        }
    })
});

$("#repr").change(function () {
    $("#plandata_show_con").html("");
});

$("#parcel").change(function () {
    $("#plandata_show_con").html("");
});

$("#sorts").change(function () {
    $("#plandata_show_con").html("");
});

//Load Crops for selection
$("#category").change(function () {
    $("#plandata_show_con").html("");
    $.ajax({
        url: "/AjaxsRequest/LoadCrops",
        method: "get",
        data: { id: $(this).val() },
        beforeSend: function () {
            $("#crops").attr("disabled", true);
        },
        type: "JSON",
        success: function (response) {
            $("#crops option").remove();
            $("#crops").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#crops").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }
        },
        complete: function () {
            $("#crops").removeAttr("disabled");
        }
    })
});

//Load Parcels for selection
$("#category").change(function () {
    $("#plandata_show_con").html("");
    $.ajax({
        url: "/AjaxsRequest/LoadParcels",
        method: "get",
        data: { id: $(this).val() },
        beforeSend: function () {
            $("#parcel").attr("disabled", true);
        },
        type: "JSON",
        success: function (response) {
            $("#parcel option").remove();
            $("#parcel").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#parcel").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }
        },
        complete: function () {
            $("#parcel").removeAttr("disabled");
        }
    })
});

//Load Sorts for selection
$("#crops").change(function () {
    $("#plandata_show_con").html("");
    $.ajax({
        url: "/AjaxsRequest/LoadCropSorts",
        method: "get",
        data: { id: $(this).val() },
        beforeSend: function () {
            $("#sorts").attr("disabled", true);
        },
        type: "JSON",
        success: function (response) {
            $("#sorts option").remove();
            $("#sorts").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#sorts").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }
        },
        complete: function () {
            $("#sorts").removeAttr("disabled");
        }
    }),
    $.ajax({
        url: "/AjaxsRequest/LoadReproduction",
        method: "get",
        data: { id: $(this).val() },
        type: "JSON",
        success: function (response) {
            $("#repr option").remove();
            $("#repr").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#repr").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }
        }
    })
});

//Show
$("#show_plan").click(function () {
    if ($("#parcel").val() && $("#repr").val() && $("#sorts").val()) {
        //$("#error_con").css("display", "none");
        $.ajax({
            url: "/AjaxsRequest/LoadPlanTable",
            method: "get",
            data: {
                rep_id: $("#repr").val(),
                sort_id: $("#sorts").val(),
                parcel_id: $("#parcel").val()
            },
            type: "JSON",
            success: function (response) {
                $("#plandata_show_con").html("");
                $("#plandata_show_con").html(response);
            }
        })
    }
    else {
        alert("Zəhmət olmasa məlumatları düzgün daxil edin!");
        //$("#error_con").css("display", "flex");
    }
});

//Save
$("#save_plan_settings").click(function () {
    if ($("#parcel").val() && $("#repr").val() && $("#sorts").val() && $("#plan_date").val() ) {
        //$("#error_con").css("display", "none");
        $.ajax({
            url: "/AjaxsRequest/SaveAllPlanData",
            method: "get",
            data: {
                repr_id: $("#repr").val(),
                sort_id: $("#sorts").val(),
                parcel_id: $("#parcel").val(),
                plan_date: $("#plan_date").val()
            },
            type: "JSON",
            success: function (response) {
                if (response == "OK") {
                    alert("Məlumatlar uğurla yadda saxlanıldı.");
                    location.reload();
                }
                else {
                    alert("Seçiminizə uyğun məlumat tapılmadı!");
                }
            }
        })
    }
    else {
        alert("Zəhmət olmasa məlumatları düzgün daxil edin!");
        //$("#error_con").css("display", "flex");
    }
});

//Show Table
$("#plan_properties").click(function () {
    $.ajax({
        url: "/AjaxsRequest/LoadPlaningPlans",
        method: "get",
        type: "JSON",
        beforeSend: function () {
            $("#loadingPlanningPlan").css("display", "block");
            $("#planning_plan_table").css("display", "none");
        },
        success: function (response) {
            $("#planning_plan_table tbody").html("");
            $("#planning_plan_table tbody").html(response);
        },
        complete: function () {
            $("#loadingPlanningPlan").css("display", "none");
            $("#planning_plan_table").css("display", "table");
        }
    })
});

//Show Plan Details
$(document).on("click",".plan_detail_opener", function () {
    $.ajax({
        url: "/AjaxsRequest/ShowPlanDetail",
        method: "get",
        type: "JSON",
        data: {
            id: $(this).data("id")
        },
        beforeSend: function () {
            $("#loadingPlanningPlanDetail").css("display", "block");
            $("#planning_detail").css("display", "none");
        },
        success: function (response) {
            $("#planning_detail").html("");
            $("#planning_detail").html(response);
        },
        complete: function () {
            $("#loadingPlanningPlanDetail").css("display", "none");
            $("#planning_detail").css("display", "flex");
        }
    })
});
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< End Crop Planning >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< MYSilo >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
$(".mysilo_info").click(function () {
    $.ajax({
        url: "/AjaxsRequest/ShowMYSiloDetail",
        method: "get",
        type: "JSON",
        data: {
            id: $(this).data("id")
        },
        beforeSend: function () {
            $("#loadingsiloDetail").css("display", "block");
            $("#silo_detail").css("display", "none");
        },
        success: function (response) {
            $("#silo_detail").html("");
            $("#silo_detail").html(response);
        },
        complete: function () {
            $("#loadingsiloDetail").css("display", "none");
            $("#silo_detail").css("display", "flex");
        }
    })
});
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< End MYSilo >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>












































