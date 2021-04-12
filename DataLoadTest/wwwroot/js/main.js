// const { data } = require("jquery");

function go2(on) {
    var box1 = on
    alert(box1);
}

function ld(databox) {
    alert(databox);
    console.log(databox);
}

function dtJson(dtJson) {
    var varjBox = dtJson;
    console.log(varjBox);
    var testdata = [{ "COLUMN1": "A-0", "COLUMN2": "B-0", "COLUMN3": "C-0", "COLUMN4": "D-0", "COLUMN5": "E-0", "COLUMN6": "F-0", "COLUMN7": "G-0", "COLUMN8": "H-0", "COLUMN9": "I-0", "COLUMN10": "J-0", "COLUMN11": "K-0", "COLUMN12": "L-0", "COLUMN13": "M-0", "COLUMN14": "N-0", "COLUMN15": "O-0" }, { "COLUMN1": "A-1", "COLUMN2": "B-1", "COLUMN3": "C-1", "COLUMN4": "D-1", "COLUMN5": "E-1", "COLUMN6": "F-1", "COLUMN7": "G-1", "COLUMN8": "H-1", "COLUMN9": "I-1", "COLUMN10": "J-1", "COLUMN11": "K-1", "COLUMN12": "L-1", "COLUMN13": "M-1", "COLUMN14": "N-1", "COLUMN15": "O-1" }, { "COLUMN1": "A-2", "COLUMN2": "B-2", "COLUMN3": "C-2", "COLUMN4": "D-2", "COLUMN5": "E-2", "COLUMN6": "F-2", "COLUMN7": "G-2", "COLUMN8": "H-2", "COLUMN9": "I-2", "COLUMN10": "J-2", "COLUMN11": "K-2", "COLUMN12": "L-2", "COLUMN13": "M-2", "COLUMN14": "N-2", "COLUMN15": "O-2" }, { "COLUMN1": "A-3", "COLUMN2": "B-3", "COLUMN3": "C-3", "COLUMN4": "D-3", "COLUMN5": "E-3", "COLUMN6": "F-3", "COLUMN7": "G-3", "COLUMN8": "H-3", "COLUMN9": "I-3", "COLUMN10": "J-3", "COLUMN11": "K-3", "COLUMN12": "L-3", "COLUMN13": "M-3", "COLUMN14": "N-3", "COLUMN15": "O-3" }, { "COLUMN1": "A-4", "COLUMN2": "B-4", "COLUMN3": "C-4", "COLUMN4": "D-4", "COLUMN5": "E-4", "COLUMN6": "F-4", "COLUMN7": "G-4", "COLUMN8": "H-4", "COLUMN9": "I-4", "COLUMN10": "J-4", "COLUMN11": "K-4", "COLUMN12": "L-4", "COLUMN13": "M-4", "COLUMN14": "N-4", "COLUMN15": "O-4" }]

    $('#loadSample').dataTable({
        ajax: "json.json",
        columns: [
            { data: "COLUMN1" },
            { data: "COLUMN2" },
            { data: "COLUMN3" },
            { data: "COLUMN4" },
            { data: "COLUMN5" },
            { data: "COLUMN6" },
            { data: "COLUMN7" },
            { data: "COLUMN8" },
            { data: "COLUMN9" },
            { data: "COLUMN10" },
            { data: "COLUMN11" },
            { data: "COLUMN12" },
            { data: "COLUMN13" },
            { data: "COLUMN14" },
            { data: "COLUMN15" },
        ]
        
    });

}