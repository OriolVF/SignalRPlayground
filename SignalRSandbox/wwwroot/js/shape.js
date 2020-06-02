$shape = $("#shape");
var connectionShape = new signalR.HubConnectionBuilder()
    .withUrl('/shapeHub')
    .build();

var initialOffset;

$(document).ready(function () {
    initialOffset = $shape.offset();
    console.log('offset: ', initialOffset);
});

connectionShape.on('shapeMoved', function (x, y) {
    $shape.css({ left: x, top: y });
    console.log('received offsets: ', x, y);
});

connectionShape.start().then(
    $shape.draggable({
        drag: function () {
            connectionShape.invoke("MoveShape", parseInt(this.offsetLeft - initialOffset.left), parseInt(this.offsetTop - initialOffset.top));
        }
    })
);