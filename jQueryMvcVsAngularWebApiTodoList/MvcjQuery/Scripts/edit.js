$(function () {

    var $tbody = $('#grid').find('tbody'); //ID selectors are less than ideal, but often necessary with jQuery

    //the template for another item, the index will replace ###
    var todoListTemplate = $('#item-template').html();

    $('#addTodo').on('click', function (e) {

        var nextItemIdx = $tbody.find('tr').length;

        //the template for another item, the index will replace ###
        var nextItem = todoListTemplate.replace(/###/g, nextItemIdx);

        $tbody.append(nextItem);
        
        return false; //block click handler bubble
    });

    $('.delete-todo').on('click', function (e) {
        var $deleteTargetRow = $(e.target).closest('tr');
        var $siblingsAfter = $deleteTargetRow.nextAll();

        $deleteTargetRow.remove();

        $siblingsAfter.each(function () {

            //shift indexes accordingly so the MVC model binder still functions server side
            //and client validation still works client side
            var $row = $(this);

            var newIndex = $row.index();
            var oldRowNbr = newIndex + 2;
            var newRowNbr = newIndex + 1;

            //re-wire up elements with ids
            //so client side validation still works
            $row.find('[id]').each(function () {
                this.id = this.id.replace("_" + oldRowNbr + "__", "_" + newRowNbr + "__");
            });

            //rewire up elements with indexor for client validation and server side model binding to work
            $row.find('[id]').each(function () {
                this.id = this.id.replace("_" + oldRowNbr + "__", "_" + newRowNbr + "__");
            });

            $row.find('[data-valmsg-for]').each(function () {
                var current = $(this).prop('data-valmsg-for');
                $(this).prop('data-valmsg-for', current.replace("[" + oldRowNbr + "]", "[" + newRowNbr + "]"));
            });

            $row.find('[name]').each(function () {
                this.name = this.name.replace("[" + oldRowNbr + "]", "[" + newRowNbr + "]");
            });

            //re-run client validation
        });

        return false; //block click handler bubble
    });
});

//global function required for MVC ajax, not ideal and could conflict with other things in a large application
function TodoListAjaxOnComplete(resp) {
    //parse the response. if it's HTML there was a validation error
    //if its empty assume success and change hash back
    
    if (resp.responseText) {
        $('#detail').html(resp.responseText); //validation errors
    } else {
        window.location.hash = "";
        window.location.reload();
    }
}