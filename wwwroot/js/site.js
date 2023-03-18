// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/**
* Template Name: PhotoFolio - v1.2.0
* Template URL: https://bootstrapmade.com/photofolio-bootstrap-photography-website-template/
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/
function test2(el) {
    console.log(el.innerHTML)
    //el.innerHTML = nrlike; 
    var idPost = el.value;
    if (el.innerHTML == "Like") {
        $.ajax({
            type: 'POST',
            url: '/likeBook/Api',
            data: JSON.stringify(idPost),
            contentType: 'application/json',
            dataType: "JSON",
            success: function (nrLikeve) {
                console.log(nrLikeve);
                document.getElementById(el.value).innerHTML = nrLikeve;
                el.innerHTML = "Unlike";
            },
            error: function(xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
            
        });
    } else {
        $.ajax({
            type: 'POST',
            url: '/unlikeBook/Api',
            data: JSON.stringify(idPost),
            contentType: 'application/json',
            dataType: "JSON",
            success: function (nrLikeve) {
                console.log(nrLikeve)
                document.getElementById(el.value).innerHTML = nrLikeve;
                el.innerHTML = "Like";
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    }
}