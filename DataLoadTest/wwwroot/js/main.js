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
    var varjBox2 = JSON.parse(dtJson);

    console.log(varjBox);

    var testdata = [['A-0', 'B-0', 'C-0', 'D-0', 'E-0', 'F-0', 'G-0', 'H-0', 'I-0', 'J-0', 'K-0', 'L-0', 'M-0', 'N-0', 'O-0'], ['A-1', 'B-1', 'C-1', 'D-1', 'E-1', 'F-1', 'G-1', 'H-1', 'I-1', 'J-1', 'K-1', 'L-1', 'M-1', 'N-1', 'O-1'], ['A-2', 'B-2', 'C-2', 'D-2', 'E-2', 'F-2', 'G-2', 'H-2', 'I-2', 'J-2', 'K-2', 'L-2', 'M-2', 'N-2', 'O-2'], ['A-3', 'B-3', 'C-3', 'D-3', 'E-3', 'F-3', 'G-3', 'H-3', 'I-3', 'J-3', 'K-3', 'L-3', 'M-3', 'N-3', 'O-3'], ['A-4', 'B-4', 'C-4', 'D-4', 'E-4', 'F-4', 'G-4', 'H-4', 'I-4', 'J-4', 'K-4', 'L-4', 'M-4', 'N-4', 'O-4']]


    $('#loadSample').dataTable({
        ...varjBox2,
        //"lengthMenu": [10, 25, 50, 75, 10000],
        scrollY: 600,   // Height 길이 조절
        scroller: {
            loadingIndicator: true // 로딩 안내 메세지 표시 여부
        },
    });

}