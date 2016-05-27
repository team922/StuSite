function htmlencode(string) {
    var div = document.createElement('div');
    div.appendChild(document.createTextNode(string));
    return div.innerHTML;
}
function htmldecode(strings) {
    var div = document.createElement('div');
    div.innerHTML = string;
    return div.innerText || div.textContent;
}