function addElementToPage(element) {

    var sizeControlElement = `  <div>
                                    <input type="button" value="Удалить" class="remove" onclick="removeElement(this)">
                                </div>`;

    var elementContainer = document.createElement("div");

    var elements = document.getElementsByClassName('inputElement');
    if (elements < 0) {
        elementContainer.innerHTML = sizeControlElement + insertCharacter(element, 0);
        elements[elements.length - 1].append(elementContainer);
    }
    else {
        elementContainer.innerHTML = sizeControlElement + insertCharacter(element, elements.length);
        document.getElementById('anchorElement').append(elementContainer)
    }

}

function insertCharacter(str, position) {
    return str.replace("Elements[]", `Elements[${position}]`);
}

function removeElement(sender) {
    sender.parentElement.parentElement.parentElement.removeChild(sender.parentElement.parentElement);
}