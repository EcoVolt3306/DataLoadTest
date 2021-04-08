// const { data } = require("jquery");

function go2(on) {
    var box1 = on
    alert(box1);
}

function ld(databox) {
    alert(databox);
    console.log(databox);
}

function dtJson(jBox) {
    var varjBox = [jBox];

    //var data = varjBox
    //console.log(varjBox);
        

        //[
        //    "OHHHHHH",
        //    "E Director",
        //    "ODS Edinburgh",
        //    "o4ero 8422",
        //    "2011/07/25",
        //    "$5,300",
        //    "Director",
        //    "Edinburgh",
        //    "8422",
        //    "dfg 2011/07/25",
        //    "$5,300",
        //    "Director",
        //    "Edinburgh",
        //    "8422",
        //    "2011/07/25"
        //],
        //[
        //    "Garrett Winters",
        //    "Director",
        //    "Edinburgh",
        //    "8422",
        //    "2011/07/25",
        //    "$5,300",
        //    "Director",
        //    "Edinburgh",
        //    "8422",
        //    "2011/07/25",
        //    "$5,300",
        //    "Director",
        //    "Edinburgh",
        //    "8422",
        //    "2011/07/25"
        //]
    

    $('#loadSample').dataTable({
        data: varjBox
    });
}