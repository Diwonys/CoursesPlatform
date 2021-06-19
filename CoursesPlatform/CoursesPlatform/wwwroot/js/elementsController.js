function addElementToPage(element) {

    var sizeControlElement = `  <div>
                                    <label for="width">width</label>
                                    <input type="number" name="width" style="width: 50px;">
                                    <label for="height">height</label>
                                    <input type="number" name="height" style="width: 50px;">
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

function removeElement() {

}