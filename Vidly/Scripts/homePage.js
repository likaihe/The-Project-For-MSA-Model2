﻿
$(document).ready(function () {
    var locationElement;
    var movieId;
    

    //dispaly the table and navagate to this pareent's div
    $(".upload-info").click(function () {
        locationElement = this.parentNode;
        console.log(locationElement);
        
    });

    //chooising movie to be uploaded
    $(".chooseMovie").click(function () {
        //change img and the title...
        var url;
        url = this.getAttribute("value-url");
        
        locationElement.getElementsByTagName("img")[0].src = url;
        locationElement.getElementsByTagName("h4")[0].innerText = this.innerText;
               
        //get the movie's id
        movieId = this.getAttribute("value-id");

        //set the movie's id in model
        var model = locationElement.getElementsByClassName("form-control")[0];
        model.setAttribute("value", movieId);

        // set the href prop in <a>
        var aTag = locationElement.getElementsByTagName("a")[0];
        aTag.setAttribute("href", "/Movies/Details/" + movieId);             
    });

    $(".mask").click(function () {
        
    });
 

});

