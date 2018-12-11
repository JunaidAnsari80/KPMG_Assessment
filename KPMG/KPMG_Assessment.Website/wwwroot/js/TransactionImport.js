var pageSize = 10;
function ImportFile() {
    
    $('#processLog').html("");
    console.log("Process start...");
    $('#processLog').append("Process start....").append("</br>");
    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx)$/;

    console.log("Checking file format");
    $('#processLog').append("Checking file format").append("</br>");
    if (regex.test($("#excelfile").val().toLowerCase())) {

        console.log("Is xlxs file");

        if (typeof (FileReader) != "undefined") {
            console.log("support HTML5");
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = e.target.result;
                console.log("Reading file");
                var workbook = XLSX.read(data, { type: 'binary' });
                console.log("getting sheets");
                var sheet_name_list = workbook.SheetNames;
                console.log(sheet_name_list);

                var sheetCount = 0; /*This is used for restricting the script to consider only first sheet of excel*/
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/
                    /*Convert the cell value to Json*/
                    console.log("Getting json data");
                    var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    console.log(exceljson);
                    if (exceljson.length > 0 && sheetCount == 0) {
                        console.log("Json array length=" + exceljson.length > 0);
                        SaveFileData(exceljson);
                        sheetCount++;
                    }
                });
                $('#exceltable').show();
            }

            reader.readAsArrayBuffer($("#excelfile")[0].files[0]);

        }
        else {
            $('#processLog').append("Error: Browser doesnot support");
        }
    }
    else {
        $('#processLog').append("Invalid file format");
    }
}

function ShowTransactions(pageIndex) {

    var webAPI = "http://localhost:62771/api/Transaction/gettransactions?pageIndex="+pageIndex+"&pageSize="+pageSize;

    $.ajax({
        url: webAPI,
        type: 'GET',        
       
        success: function (data) {
            console.log(data);
            $('#divTransactions').html(data);
        },
        error: function (e, x, y) {
            console.log("error occured");
            console.log(e);
            console.log(x);
            console.log(y);
        },
        done: function (doc) {
            //$('#container').html(doc);
        }

    });
}

function SaveFileData(jsonData) {
  
    $('#processLog').append("File row count: " + jsonData.length).append("</br>");
    $('#processLog').append("Chunking file").append("</br>");

    var tempArray = chunkArray(jsonData, 5000);
    $('#processLog').append("Total Chunks:" + tempArray.length).append("</br>");
    console.log("temparraylenth:" + tempArray.length);
    for (f = 0; f < tempArray.length; f++) {
        $('#processLog').append("Processing Chunk:" + f).append("</br>");
        
        data = JSON.stringify(tempArray[f]);
       
        var webAPI = "http://localhost:62771/api/Transaction/upload";
       
        $.ajax({
            url: webAPI,
            type: 'POST',
            data: data,
            contentType: "application/json",
            async: false,
            cache: true,
            success: function (doc) {            
                for (i = 0; i < doc.response.length; i++) {  
                    $('#processLog').append("Error on row " + doc.response[i].rowNumber).append("</br>");
                    var responseErrors = doc.response[i].errors;
                    for (j = 0; j < responseErrors.length; j++) {
                        $('#processLog').append(responseErrors[j].errorMessage).append("</br>");
                      
                    }
                }
                $('#processLog').append("Processing Chunk:" + f +" completed").append("</br>");
                console.log(doc.response);
            },
            error: function (e, x, y) {
                console.log(e);
                console.log(x);
                console.log(y);
            },
            done: function (doc) {
                console.log('done calling show transactions');
                ShowTransactions();
            }

        });
    }

    console.log('done calling show transactions');
    
    ShowTransactions(1);
}


function chunkArray(myArray, chunk_size) {
    var results = [];

    while (myArray.length) {
        results.push(myArray.splice(0, chunk_size));
    }

    return results;
}

function BindTable(jsondata, tableid) {/*Function used to convert the JSON array to Html Table*/
    var columns = BindTableHeader(jsondata, tableid); /*Gets all the column headings of Excel*/
    for (var i = 0; i < jsondata.length; i++) {
        var row$ = $('<tr/>');
        for (var colIndex = 0; colIndex < columns.length; colIndex++) {
            var cellValue = jsondata[i][columns[colIndex]];
            if (cellValue == null)
                cellValue = "";
            row$.append($('<td/>').html(cellValue));
        }
        $(tableid).append(row$);
    }
}
function BindTableHeader(jsondata, tableid) {/*Function used to get all column names from JSON and bind the html table header*/
    var columnSet = [];
    var headerTr$ = $('<tr/>');
    for (var i = 0; i < jsondata.length; i++) {
        var rowHash = jsondata[i];
        for (var key in rowHash) {
            if (rowHash.hasOwnProperty(key)) {
                if ($.inArray(key, columnSet) == -1) {/*Adding each unique column names to a variable array*/
                    columnSet.push(key);
                    headerTr$.append($('<th/>').html(key));
                }
            }
        }
    }
    $(tableid).append(headerTr$);
    return columnSet;
}
