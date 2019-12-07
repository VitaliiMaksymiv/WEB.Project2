$(document).ready(function () {
    $('#addInput').click(function () {
        var length = $('#toAdd').children().length;
        $('#toAdd').append('<div class="form-group form-inline"> <input type = "hidden" name = "Authors[' + length + '].AuthorId" class= "form-control" /> <input type="text" name="Authors[' + length + '].FirstName" required class="form-control"/><input type="text" name="Authors[' + length + '].LastName" required class="form-control mx-2"/><button type="button" class="btn btn-sm btn-danger z-depth-0 form-control m-0" onclick="RemoveInput(this)"><i class="fas fa-times"></i></button> </div>');
    });
    $('#orderBy').change(function () {
        $(this).parents('form').submit();
    });

});

function RemoveInput(button) {
    $(button).parent().remove();
}

function Submit(select) {
    $(select).parents('form').submit();
}

function Next() {
    var input = $('#pageNumber');
    input.val(Number(input.val()) + 1);
    input.parents('form').submit();
}

function Prev() {
    var input = $('#pageNumber');
    input.val(input.val() - 1)
    input.parents('form').submit();
}

function Find() {
    $('#pageNumber').val(1);
}