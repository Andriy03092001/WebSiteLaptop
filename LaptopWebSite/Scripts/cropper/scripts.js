$(function () {
    var cropper = function () {
        //Чи открито діалогово вікно для кропу
        var isCropped = false;
        var loader = {
            //Для початку анімації загрузки фото
            show: function () {
                $("#loading").show();
            },
            //Для закривання анімації загрузки фото
            hide: function () {
                $("#loading").hide();
            }
        };
        //Діалогове вікно для обрізки фото
        var dialog = {
            //Шоб воно було як на головному екрані
            body: document.querySelector('body'),
            //Для відкриття самого кропу
            show: function () {
                this.body.classList.toggle("open");
                $(".containerCrop").show();
                $(".navbar").hide();
            },
            //Для закривання самого кропу
            hide: function () {
                this.body.classList.remove("open");
                $(".containerCrop").hide();
                $(".navbar").show();
            }
        };
        //Діалогове вікно для обрізки фото
        var server = {
            sendImage: function (base64) {
                var form = $('#__AjaxAntiForgeryForm');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                //var imageCurrent = "#imageContainerPlus" + counterImages;

                //var image = $(imageCurrent).find(".uploadimage")[0];
                $.ajax({
                    url: "/Product/UploadBase64",
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                        base64image: base64
                    },
                    success: function (result) {
                        var itemImage = '';

                        var $divImages = $("#divImages");
                        var hidden2 = "<input type='hidden' id='ProductImages' name='ProductImages' value='" + result.image + "'/> ";
                        $divImages.append(hidden2);
                        
                        itemImage += '<div class="col-md-2 uploadImage"  data-id="' + result.image + '">' +
                            '<div class="thumbnail">' +
                            '<i class="fa fa-times fa-2x icon-delete" aria-hidden="true"></i>' +
                            '<img src="' + result.imagePath + '" class="uploadimage" alt = "Lights" style = "width:100%" >' +
                            '<div class="caption"><p>Add New Image</p></div>' +
                            '</div>' +
                            '</div>';
                        $("#listUploadImages").append(itemImage);
                        //$("#imageContainerPlus").insertBefore(itemImage);

                        //image.src = result.imagePath;
                        //console.log();
                    }
                });
            },
            deleteImage: function (id) {
                var form = $('#__AjaxAntiForgeryForm');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                //var imageCurrent = "#imageContainerPlus" + counterImages;

                //var image = $(imageCurrent).find(".uploadimage")[0];
                $.ajax({
                    url: "/Product/DeleteImageAjax",
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                        id: id
                    },
                    success: function (result) {
                        if (result.success) {
                            var divDelete = $("#listUploadImages")
                                .find("[data-id='" + id + "']").first();
                            divDelete.remove();
                        }


                    }
                });
            }
        };
        //Ініціалізація кропера та його кнопок
        function init() {
            //Ініціалізація кнопок та самого кропу
            initButtonControls();
            initEventBaseClick();

            //Базові кнопакі діалога
            function initEventBaseClick() {

                //Закрити діалог кропера
                $("#cropperClose").on("click", function () {
                    dialog.hide();
                });

                //Обрізати фото натиснули
                $("#crop").click(function () {
                    cropped();
                });

                //Додаємо нову фотку на сайт
                $("#imageContainerPlus").on('click', function () {
                    var inputFile = $('<input/>')
                        .attr('type', 'file')
                        .attr('name', 'img_file')
                        .attr('id', 'img_file')
                        .attr('class', 'hide');

                    $("#fileUploadContainer").html("");

                    $("#fileUploadContainer")
                        .html(inputFile);
                    inputFile.click();

                    //Вибір нового малюнка
                    inputFile.on('change', function () {
                        if (this.files && this.files[0]) {
                            if (this.files[0].type.match(/^image\//)) {
                                onLoad(this.files[0]);
                            }
                            else {
                                alert("Invalid file type");
                            }
                        }
                        else {
                            alert("Please select a file.");
                        }

                    });
                    return false;
                });

                //Видаляємо фото
                $("#listUploadImages").on('click', '.icon-delete', function () {
                    var div = $(this).closest("[data-id], .plusupload");
                    var id = div.attr("data-id");
                    server.deleteImage(id);
                });
            }

            //Ініціалізація конопок самого кропера поворот
            function initButtonControls() {

                $("#dragModeMove").on("click", function () {
                    $('#canvas').cropper('setDragMode', 'move');
                });

                $("#dragModeCrop").on("click", function () {
                    $('#canvas').cropper('setDragMode', 'crop');
                });
                var x = 1;
                $("#scaleX").on("click", function () {
                    $('#canvas').cropper('scaleX', x *= -1);

                });
                var y = 1;
                $("#scaleY").on("click", function () {
                    $('#canvas').cropper('scaleY', y *= -1);
                });

                $("#rotateLeft").on("click", function () {
                    $('#canvas').cropper('rotate', 45);
                });

                $("#rotateRight").on("click", function () {
                    $('#canvas').cropper('rotate', -45);
                });

                $("#moveLeft").on("click", function () {
                    $('#canvas').cropper('move', -50, 0);
                });

                $("#moveRight").on("click", function () {
                    $('#canvas').cropper('move', 50, 0);
                });

                $("#moveUp").on("click", function () {
                    $('#canvas').cropper('move', 0, -50);
                });

                $("#moveDown").on("click", function () {
                    $('#canvas').cropper('move', 0, 50);
                });

                $("#zoomPlus").on("click", function () {
                    $('#canvas').cropper('zoom', 0.1);
                });

                $("#zoomMinus").on("click", function () {
                    $('#canvas').cropper('zoom', -0.1);
                });
            }
        }
        //Запуск ініціалізації
        function run() {
            init();
        }
        //Загрузка малюнка в кропер
        function onLoad(fileImage) {
            var $canvas = $("#canvas"),
                context = $canvas.get(0).getContext('2d');
            //Включити -----------
            loader.show();
            var reader = new FileReader();
            //Закінчення анімації та виведення картинки в всіх пропорціях
            reader.onload = function (e) {
                //Виключити ---------
                var img = new Image();
                img.onload = function () {

                    loader.hide();
                    context.canvas.width = img.width;
                    context.canvas.height = img.height;

                    if (img.width <= 300 && img.height <= 300) {
                        alert("Ображення менше 300 пікселів");
                        return;
                    }
                    //Показуємо діалог
                    dialog.show();
                    isCropped = true;

                    context.drawImage(img, 0, 0);
                    var cropper = $canvas.cropper('destroy').cropper({
                        aspectRatio: 16 / 9,
                        viewMode: 0,
                        dragMode: 'move',
                        preview: '.img-preview',
                        //autoCropArea: 0.00000001,
                        //aspectRatio: 2,
                        //,
                        crop: function (e) {
                            var data = e.detail;
                            var h = Math.round(data.height);
                            var w = Math.round(data.width);
                            if (w <= 300 && isCropped) {
                                this.cropper.setData({ width: 300 });
                            }
                            //else
                        }
                    });
                };
                img.src = e.target.result;
            };
            reader.readAsDataURL(fileImage);
        }

        //якшо картинка вже скропана то вивести її на ліст та сховати діалого вікно з кропером
        function cropped() {
            if (isCropped) {
                var $canvas = $("#canvas");
                var croppedImage = $canvas.cropper('getCroppedCanvas').toDataURL('image/png');
                server.sendImage(croppedImage.split(',')[1]);
                dialog.hide();
                isCropped = false;
            }
        }
        return { start: run };
    }();
    //Запуск кропера
    cropper.start();
});