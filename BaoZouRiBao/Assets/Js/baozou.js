function getImgs()
{
    var imgs = document.getElementsByTagName("img");
    notify(JSON.stringify(imgs));
}

function notify(parameter)
{
    window.external.notify(parameter);
}