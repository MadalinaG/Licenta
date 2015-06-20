﻿$(document).ready(function () {
    $(".pager").click(function (evt) {
        var pageindex = evt.target.innerHTML;
        $("#CurrentPageIndex").val(pageindex);
        evt.preventDefault();
        $("form").submit();
    });

    //$('.po-markup > .po-link').popover({
    //    trigger: 'hover',
    //    html: true,  // must have if HTML is contained in popover

    //    // get the title and conent
    //    title: function () {
    //        return $(this).parent().find('.po-title').html();
    //    },
    //    content: function () {
    //        return $(this).parent().find('.po-body').html();
    //    },

    //    container: 'body',
    //    placement: 'right'

    //});

    $(".start").click(function (evt) {
        var elem = document.getElementById("test").value;
        evt.preventDefault();
        $("form").submit();
    });


    var topics = document.getElementsByClassName("topics");
    $('#topName').change(function () {
        var length = topics.length;
        var userTopic = document.getElementById("topName").value;
        for (var i = 0; i < length; i++) {
            if (topics[i].defaultValue == userTopic) {
                $(".topicName").append('<span id="TopicName-error" class="">This topic already exists in the database.</span>');
            }
        }

        if (userTopic == "") {
            $(".topicName").remove();
        }
    });

    //$('form input').change(function () {
    //    $('form p').text(this.files.length + " file selected");
    //});
    $("#mytable #checkall").click(function () {
        if ($("#mytable #checkall").is(':checked')) {
            $("#mytable input[type=checkbox]").each(function () {
                $(this).prop("checked", true);
            });

        } else {
            $("#mytable input[type=checkbox]").each(function () {
                $(this).prop("checked", false);
            });
        }
    });

});
   //$(function(){

   //    $(".movie").click(function(e){
   //        e.preventDefault();
   //        $.post("@Url.Action("Delete","Movie")", { id : $(this).data("movieId")} ,function(data){
   //            alert(data);
   //        });
   //    });
   //});
  
jQuery(function ($) {
    $("[data-toggle=tooltip]").tooltip();
});

$(function () {
    $(document).on('focus', 'div.form-group-options div.input-group-option:last-child input', function () {
        var sInputGroupHtml = $(this).parent().html();
        var sInputGroupClasses = $(this).parent().attr('class');
        $(this).parent().parent().append('<div class="' + sInputGroupClasses + '">' + sInputGroupHtml + '</div>');
    });

    $(document).on('click', 'div.form-group-options .input-group-addon-remove', function () {
        $(this).parent().remove();
    });
});