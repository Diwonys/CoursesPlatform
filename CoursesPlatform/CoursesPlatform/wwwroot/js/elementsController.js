function addElementToPage(element) {

    var sizeControlElement = `  <div>
                                    <label for="width">width</label>
                                    <input type="number" name="width" style="width: 50px;">
                                    <label for="height">height</label>
                                    <input type="number" name="height" style="width: 50px;">
                                </div>`;

    var elementContainer = document.createElement("div");
    elementContainer.innerHTML = sizeControlElement + element;

    var elements = document.getElementsByClassName('inputElement');
    if (elements < 0) {
        elements[elements.length - 1].append(elementContainer);
    }
    else{
        document.getElementById('anchorElement').append(elementContainer)
    }
}
function removeElement() {

}