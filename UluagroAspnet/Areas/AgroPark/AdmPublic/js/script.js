//script2

//Change password
$("#_changePassword").on("click", function () {
    var newpassword = $("#_newPassword").val();
    $.ajax({
        url: '/Admin/ChangePassword',
        data: { newpassword: newpassword },
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response)
        }
    })
})


    // <<<<<<<<<<<<<<<<<<<<<<<<<<<<  STOCK  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


//medaxil ve mexaric
$("#_type").on("change", function () {
    $("#_category").removeAttr("disabled");
    var type = this.value;
    var category = $("#_category").val();
    $("#_quantity").val(""); 
    StockAdded(type, category);
    $("#_stockplace").attr("disabled","disabled");
    $("#_stockplace option").remove();
    $("#_stockplace").append("<option selected disabled value=''>Sec</option>");
       
});

// kategoriyalar deyismeleri
$("#_category").on("change", function () {
    var type = $("#_type").val();
    var category = this.value;
    $("#_quantity").val("");
    StockAdded(type, category);
    $("#_stockplace").attr("disabled","disabled");
    $("#_stockplace option").remove();
    $("#_stockplace").append("<option selected disabled value=''>Sec</option>");
   
});

//emeliyyatlar deyisdikce sonradan elave olunan inputlarin silinmesi.
function TechnicalFormatClear() {
    $("#_quantity").val(1);
    $('#_quantity').attr('readonly', true);
    $("#_technicalLicensePlate").parent().remove();
    $("#_technicalColor").parent().remove();
}

//butun saxlanma yerlerinin ve ya lazim olan anbarlari cekib getirmek.
function AllStockPlaces(_id) {    
    $.ajax({
        url: '/STOCK/StockPlaces',
        data: { id: _id },
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response);
            $("#_stockplace option").remove();
            $("#_stockplace").append("<option selected disabled value=''>Sec</option>")
            if (response != null || response != false) {
                for (var i = 0; i < response.length; i++) {
                    if (response[i].QUANTITYTOTAL!=0) {
                        $("#_stockplace").append("<option value='" + response[i].OBJECTID + "'>" + response[i].STOCKPLACENAME + "   (" + response[i].QUANTITYTOTAL + ")</option>");
                    }
                    else {
                        $("#_stockplace").append("<option value='" + response[i].OBJECTID + "'>" + response[i].STOCKPLACENAME+"</option>");
                    }
                }
            }
        }
    })
}

//mehsul deyisdikce saxlama yerlerinin deyisdirelmesi
$("#_productName").on("change", function () {
    $("#_stockplace").removeAttr("disabled");
    if ($("#_type").val()==2) {
        AllStockPlaces(this.value);
    }
    else {
        AllStockPlaces(0);
    }
})

//teyinata ve categoriyaya gore emeliyatlarin formalari
function StockAdded(typevalue, categoryvalue ) {
    $("#_productName option").remove();
    $("#_productName").append("<option selected disabled value=''>Sec</option>");
    // secilen eger gubre ve ya dermandirsa
    if ((categoryvalue == 1 || categoryvalue == 2) && typevalue==1) {
        $("#_quantity").val("");
        $("#_quantity").removeAttr("readonly")
        $("#_techicalCategory").parent().css("display", "none");
        $("#_technicalLicensePlate").parent().remove();
        $("#_technicalColor").parent().remove();
        $("#_cropDetals div").remove();
        $("#_cropRepreduc div").remove();
        $.ajax({
            url: '/STOCK/GetFeltizilerByCATID',
            data: { catid: categoryvalue },
            method: "post",
            type: "JSON",
            success: function (response) {
                console.log(response)
                $("#_productName option").remove();
                $("#_productName").append("<option selected disabled value=''>Sec</option>")
                if (response != null || response != false) {
                    for (var i = 0; i < response.length; i++) {
                        $("#_productName").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                    }
                }
            }
        })
    }    
    if ((categoryvalue == 1 || categoryvalue == 2) && typevalue == 2) {
        $("#_quantity").removeAttr("readonly");
        $("#_productName option").remove();
        $("#_productName").append("<option selected disabled value=''>Sec</option>")
        $("#_cropDetals div").remove();
        $("#_cropRepreduc div").remove();
        $.ajax({
            url: '/STOCK/GetStockResursFelt',
            data: { id: categoryvalue },
            method: "post",
            type: "JSON",
            success: function (response) {
                console.log(response)
                $("#_productName option").remove();
                $("#_productName").append("<option selected disabled value=''>Sec</option>")
                if (response != null || response != false) {
                    for (var i = 0; i < response.length; i++) {
                        $("#_productName").append("<option value='" + response[i].OBJECTID + "'>" + response[i].PRODUCTNAME + "</option>")
                    }
                }
            }
        })
    }
    //secilen eger texnikadirsa
    if (categoryvalue == 3 && typevalue == 1) {
        TechnicalFormatClear();
        $("#_cropDetals div").remove();
        $("#_cropRepreduc div").remove();
        $("#_techicalCategory").parent().css("display", "block");      

        var texnicalContent = "<div class='col-md-4 mb-4'>"
            + "<label class='control-label' for='_technicalLicensePlate'>Texnikanın Nömrəsi</label>"
            + "<input id='_technicalLicensePlate' class='form-control' name='SPECS' required />"
            + "</div>"
            + "<div class='col-md-4 mb-4'>"
            + "<label class='control-label' for='_technicalColor'>Texnikanın Rengi</label>"
            + "<input id='_technicalColor' class='form-control' name='SPECS' required />"
            + "</div>"
        $("#_StorkFormGroup").append(texnicalContent);
    }
    if (categoryvalue == 3 && typevalue == 2) {
        TechnicalFormatClear();
        $("#_cropDetals div").remove();
        $("#_cropRepreduc div").remove();
        $("#_techicalCategory").parent().css("display", "none");        
        $.ajax({
            url: '/STOCK/GetStockResursTech',
            data: { id: categoryvalue },
            method: "post",
            type: "JSON",
            success: function (response) {
                console.log(response)
                $("#_productName option").remove();
                $("#_productName").append("<option selected disabled value=''>Sec</option>")
                if (response != null || response != false) {
                    for (var i = 0; i < response.length; i++) {
                        $("#_productName").append("<option value='" + response[i].OBJECTID + "'>" + response[i].PRODUCTNAME + "</option>")
                    }
                }
            }
        })
    }
    
    // secilen eger denli bitki ve ya meyvedirse
    if ((categoryvalue == 4 || categoryvalue == 5) && typevalue == 1) {
        console.log("bitki ve ya meyve")
        $("#_productName option").remove();
        $("#_productName").append("<option selected disabled value=''>Sec</option>")
        $("#_techicalCategory").parent().css("display", "none");
        $("#_technicalLicensePlate").parent().remove();
        $("#_technicalColor").parent().remove();

        var cropContent = "<div class='col-md-4 mb-4'>"
            + "<label class='control-label' for='_allCrops'>Bitkinin adı</label>"
            + "<select id='_allCrops' class='form-control' name='SPECS' required ></select>"
            + "</div>"

        $("#_cropDetals div").remove();
        $("#_cropDetals").append(cropContent);



        $.ajax({
            url: '/STOCK/GetAllCropsByCatID',
            data: { id: categoryvalue },
            method: "post",
            type: "JSON",
            success: function (response) {
                console.log(response)
                $("#_allCrops option").remove();
                $("#_allCrops").append("<option selected disabled value=''>Sec</option>")
                if (response != null || response != false) {
                    for (var i = 0; i < response.length; i++) {
                        $("#_allCrops").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                    }
                }
            }
        })
        
    }
    if ((categoryvalue == 4 || categoryvalue == 5) && typevalue==1) {
        var cropContent = "<div class='col-md-4 mb-4'>"
            + "<label class='control-label' for='_allRepreduction'>Keyfiyyət dərəcəsi</label>"
            + "<select id='_allRepreduction' class='form-control' name='SPECS' required ></select>"
            + "</div>"
        $("#_cropRepreduc div").remove();
        $("#_cropRepreduc").append(cropContent);

        $.ajax({
            url: "/STOCK/GetCropRepreducsiya",
            method: "post",
            type: "JSON",
            success: function (response) {
                $("#_allRepreduction option").remove();
                $("#_allRepreduction").append("<option selected disabled value=''>Sec</option>")
                if (response != null || response != false) {
                    for (var i = 0; i < response.length; i++) {
                        $("#_allRepreduction").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                    }
                }
            }
        })
    }
    if ((categoryvalue == 4 || categoryvalue == 5) && typevalue == 2) {

        $("#_productName option").remove();
        $("#_productName").append("<option selected disabled value=''>Sec</option>")
        $("#_techicalCategory").parent().css("display", "none");
        $("#_technicalLicensePlate").parent().remove();
        $("#_technicalColor").parent().remove();
        $("#_cropDetals div").remove();
        $("#_cropRepreduc div").remove();
        $.ajax({
            url: "/STOCK/GetOutCropStok",
            data: { id: categoryvalue },
            method: "post",
            type: "JSON",
            success: function (response) {
                $("#_productName option").remove();
                $("#_productName").append("<option selected disabled value=''>Sec</option>")
                if (response != null || response != false) {
                    for (var i = 0; i < response.length; i++) {
                        $("#_productName").append("<option value='" + response[i].OBJECTID + "'>" + response[i].PRODUCTNAME + "</option>")
                    }
                }
            }
        })
    }
}


//bitkilere gore sortlarini getirmek
$(document).on("change", "#_allCrops", function () {
    console.log(this.value)
    var cropid = this.value;
    $.ajax({
        url: '/STOCK/GelSortByCropID',
        data: { id: cropid },
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response)
            $("#_productName option").remove();
            $("#_productName").append("<option selected disabled value=''>Sec</option>")
            if (response != null || response != false) {
                for (var i = 0; i < response.length; i++) {
                    $("#_productName").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }
        }
    })
})

//repreducsiyalar


$("#_techicalCategory").on("change", function () {
    $("#_productName option").remove();
    $("#_productName").append("<option selected disabled value=''>Sec</option>");
    $.ajax({
        url: '/STOCK/GetTechnical',
        data: { techcatid: this.value },
        method: "post",
        type: "JSON",
        success: function (response) {
            console.log(response)
            $("#_productName option").remove();
            $("#_productName").append("<option selected disabled value=''>Sec</option>")
            if (response != null || response != false) {
                for (var i = 0; i < response.length; i++) {
                    $("#_productName").append("<option value='" + response[i].OBJECTID + "'>" + response[i].NAME + "</option>")
                }
            }
        }
    })
})

