//Load worker for Profession
$("#file-profession").change(function () {
    $.ajax({
        url: "/Document/LoadWorkerForProfession",
        method: "get",
        data: { id: $(this).val() },
        beforeSend: function () {
            $("#file-worker").attr("disabled", true);
        },
        type: "JSON",
        success: function (response) {
            $("#file-worker option").remove();
            $("#file-worker").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#file-worker").append("<option value='" + response[i].ID + "'>" + response[i].Name + ' ' + response[i].Surname + "</option>");
                }
            }
        },
        complete: function () {
            $("#file-worker").removeAttr("disabled");
        }
    })
});

//Send to receiver
$("#send_document").click(function () {
    if ($("#file-profession").val() && $("#file-worker").val() && $("#file-content").val() && $("#file-title").val()) {
        $.ajax({
            url: "/Document/SendMessage",
            method: "POST",
            beforeSend: function () {
                $("#sending_doc_msg").css("display", "flex");
            },
            data:
            {
                content: $("#file-content").val(),
                title: $("#file-title").val(),
                note: $("#file-note").val(),
                receiverid: $("#file-worker").val()
            },
            success: function (response) {
                if (response == "OK") {
                    alert("Mesaj uğurla göndərildi.")
                    location.reload();
                }
            },
            complete: function () {
                $("#sending_doc_msg").css("display", "none");
            }
        })
    }
    else {
        alert("Zəhmət olmasa məlumatları düzgün daxil edin!");
    }
});

//Show recived messages
$("#show_recived_msgs").click(function () {
    $.ajax({
        url: "/Document/ShowRecivedMessages",
        method: "GET",
        beforeSend: function () {
            $("#loading_rec_msg").css("display", "block");
            $("#recived_doc_msgs").css("display", "none");
        },
        success: function (response) {
            $("#recived_doc_msgs").html("");
            $("#recived_doc_msgs").html(response);
        },
        complete: function () {
            $("#loading_rec_msg").css("display", "none");
            $("#recived_doc_msgs").css("display", "block");
        }
    })
});

//Show document content
$(document).on("click", ".show_msg_content", function () {
    $.ajax({
        url: "/Document/ShowMessageContent",
        method: "GET",
        data: {
            id: $(this).data("id")
        },
        beforeSend: function () {
            $("#sending_doc_to_more").css("display", "flex");
        },
        success: function (response) {
            $("#doc_msg_content").html("");
            $("#doc_msg_content").html(response);
        },
        complete: function () {
            $("#sending_doc_to_more").css("display", "none");
        }
    })
});

//Show recived messages
$("#show_sended_msgs").click(function () {
    $.ajax({
        url: "/Document/ShowSendedMessages",
        method: "GET",
        beforeSend: function () {
            $("#loading_send_msg").css("display", "block");
            $("#sended_doc_msgs").css("display", "none");
        },
        success: function (response) {
            $("#sended_doc_msgs").html("");
            $("#sended_doc_msgs").html(response);
        },
        complete: function () {
            $("#loading_send_msg").css("display", "none");
            $("#sended_doc_msgs").css("display", "block");
        }
    })
});

//Inferior Document
$(document).on("click", "#inferior_doc", function () {
    $(this).attr("disabled", true);
    $.ajax({
        url: "/Document/İnferiorDocument",
        method: "get",
        type: "JSON",
        success: function (response) {
            $("#inf_doc_con").append(response);
        }
    })
})

//Load workers for profession
$(document).on("change", "#inf_doc_profession", function () {
    $.ajax({
        url: "/Document/LoadWorkerForProfession",
        method: "get",
        data: { id: $(this).val() },
        beforeSend: function () {
            $("#inf_doc_worker").attr("disabled", true);
        },
        type: "JSON",
        success: function (response) {
            $("#inf_doc_worker option").remove();
            $("#inf_doc_worker").append("<option selected disabled value=''>Seçin</option>");
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    $("#inf_doc_worker").append("<option value='" + response[i].ID + "'>" + response[i].Name +' '+ response[i].Surname + "</option>")
                }
            }
        },
        complete: function () {
            $("#inf_doc_worker").removeAttr("disabled");
        }
    })
});

//Array of recivers
var recivers = [];

//Add reciver
$(document).on("click", "#doc_rec_add", function () {
    var id = $("#inf_doc_worker").val();
    var fullname = $("#inf_doc_worker option:selected").text();
    if ($("#inf_doc_worker").val()) {
        if (!recivers.includes(id)) {
            recivers.push(id);
            $(".reciver_con").append("<span data-value='" + id + "' class='reciver_sp'>" + fullname + " <i class='far fa-times-circle'></i></span>")
            $(".send_accept").html("");
            if (recivers.length != 0) {
                $(".send_accept").append("<button type='button' class='btn xs bg-green' id='send_msg'>Seçilmiş şəxslərə göndər.</button>");
            }
        }
    }
});

//Remove reciver
$(document).on("click", ".reciver_sp i", function () {
    var id = $(this).parent().data("value");
    recivers.pop(id);
    $(this).parent().remove();
    $(".send_accept").html("");
    if (recivers.length != 0) {
        $(".send_accept").append("<button type='button' class='btn xs bg-green' id='send_msg'>Seçilmiş şəxslərə göndər.</button>");
    }
});

//Send to Selected Recivers
$(document).on("click", "#send_msg", function () {
    $.ajax({
        url: "/Document/SendDocToMoreUsers",
        method: "POST",
        data: {
            users: recivers,
            docid: $("#doc_id").val()
        },
        beforeSend: function () {
            $("#sending_doc_to_more").css("display", "flex");
        },
        type: "JSON",
        success: function (response) {
            if (response == "OK") {
                alert("Mesaj seçilmiş şəxslərə uğurla göndərildi.")
                location.reload();
            }
        },
        complete: function () {
            $("#sending_doc_to_more").css("display", "none");
        }
    })
});

//Accept Document
$(document).on("click", "#accept_doc", function () {
    $.ajax({
        url: "/Document/AcceptDoc",
        method: "POST",
        data: {
            doc_msg_id: $("#doc_msg_id").val(),
            note: $("#file-note-from").val()
        },
        beforeSend: function () {
            $("#sending_doc_to_more").css("display", "flex");
        },
        type: "JSON",
        success: function (response) {
            if (response == "OK") {
                alert("Cavab uğurla göndərildi.")
                location.reload();
            }
        },
        complete: function () {
            $("#sending_doc_to_more").css("display", "none");
        }
    })
});

//Decline Document
$(document).on("click", "#decline_doc", function () {
    $.ajax({
        url: "/Document/DeclineDoc",
        method: "POST",
        data: {
            doc_msg_id: $("#doc_msg_id").val(),
            note: $("#file-note-from").val()
        },
        beforeSend: function () {
            $("#sending_doc_to_more").css("display", "flex");
        },
        type: "JSON",
        success: function (response) {
            if (response == "OK") {
                alert("Cavab uğurla göndərildi.")
                location.reload();
            }
        },
        complete: function () {
            $("#sending_doc_to_more").css("display", "none");
        }
    })
});

//Show Archive Documents
$("#show_arc_docs").click(function () {
    $.ajax({
        url: "/Document/ShowArchiveDocuments",
        method: "POST",
        beforeSend: function () {
            $("#loading_arc_doc").css("display", "flex");
        },
        type: "JSON",
        success: function (response) {
            $("#arch_file_con").html("");
            $("#arch_file_con").html(response);
        },
        complete: function () {
            $("#loading_arc_doc").css("display", "none");
        }
    })
});