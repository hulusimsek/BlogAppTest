$(function () {

    // Özel Telefon Validator (+90 dışındaki TR formatı)
    $.validator.addMethod("phoneTR", function (value, element) {
        if (this.optional(element)) return true;
        let numbers = value.replace(/\D/g, "");
        if (numbers.startsWith("0")) numbers = numbers.substring(1);
        return numbers.length === 10;
    }, "Telefon numarası geçersiz formatta.");

    // FORM VALIDATION
    $("#contactForm").validate({
        ignore: [], // gizli alanları da dahil et
        rules: {
            "ContactRequest.ServiceCategoryId": {
                required: true
            },
            "ContactRequest.FullName": {
                required: true,
                minlength: 3
            },
            "ContactRequest.Email": {
                required: true,
                email: true
            },
            "ContactRequest.Subject": {
                required: true,
                minlength: 3
            },
            "ContactRequest.Message": {
                required: true,
                minlength: 10
            },
            "PhoneDisplay": {
                phoneTR: true
            },
            "ContactRequest.KvkkAccepted": {
                required: true
            }

        },
        messages: {
            "ContactRequest.ServiceCategoryId": {
                required: "Danışmanlık konusu seçimi zorunludur."
            },
            "ContactRequest.FullName": {
                required: "Ad Soyad alanı zorunludur.",
                minlength: "Lütfen en az 3 karakter girin."
            },
            "ContactRequest.Email": {
                required: "E-posta adresi zorunludur.",
                email: "Lütfen geçerli bir e-posta adresi girin."
            },
            "ContactRequest.Subject": {
                required: "Konu zorunludur.",
                minlength: "En az 3 karakter olmalı."
            },
            "ContactRequest.Message": {
                required: "Mesaj zorunludur.",
                minlength: "Mesaj en az 10 karakter olmalı."
            },
            "PhoneDisplay": {
                phoneTR: "Telefon numarası geçersiz formatta."
            },
            "ContactRequest.KvkkAccepted": {
                required: "KVKK metnini okuyup onaylamanız zorunludur."
            }

        },
        errorPlacement: function (error, element) {
            // ASP.NET span'ları (asp-validation-for) kullan
            let fieldName = element.attr("name");
            let $span = $('[data-valmsg-for="' + fieldName + '"]');

            // Eğer özel PhoneDisplay için ayrı span varsa onu hedef al
            if (element.attr("id") === "PhoneDisplay") {
                $span = $('[data-valmsg-for="PhoneDisplay"]');
            }

            if ($span.length) {
                $span.html(error.text())
                    .removeClass("field-validation-valid")
                    .addClass("field-validation-error");
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element) {
            $(element).addClass("border-red-500").removeClass("border-gray-200");
        },
        unhighlight: function (element) {
            $(element).removeClass("border-red-500").addClass("border-gray-200");
            let fieldName = $(element).attr("name");
            let $span = $('[data-valmsg-for="' + fieldName + '"]');
            if ($span.length) {
                $span.html("")
                    .removeClass("field-validation-error")
                    .addClass("field-validation-valid");
            }
        },
        submitHandler: function (form) {
            let displayInput = $(form).find("#PhoneDisplay");
            let hiddenInput = $(form).find("#Phone");

            // +90 ekle ve rakam-only formatla
            if (displayInput.val().trim() !== "") {
                let numbers = displayInput.val().replace(/\D/g, "");
                if (numbers.startsWith("0")) numbers = numbers.substring(1);
                if (numbers.length !== 10) {
                    displayInput.focus();
                    return false;
                }
                hiddenInput.val("+90" + numbers);
            }

            // Butonu disable et
            $(form).find('button[type="submit"]').prop('disabled', true).addClass("loading");

            // Native submit (sonsuz döngüyü önlemek için)
            form.submit();
        }
    });

    // Telefon input formatlama
    $("#PhoneDisplay").on("input", function () {
        let input = this;
        let numbers = input.value.replace(/\D/g, "");
        if (numbers.startsWith("0")) numbers = numbers.substring(1);
        if (numbers.length > 10) numbers = numbers.substring(0, 10);

        let formatted = "";
        if (numbers.length <= 3) formatted = numbers;
        else if (numbers.length <= 6) formatted = `${numbers.slice(0, 3)} ${numbers.slice(3)}`;
        else if (numbers.length <= 8) formatted = `${numbers.slice(0, 3)} ${numbers.slice(3, 6)} ${numbers.slice(6)}`;
        else formatted = `${numbers.slice(0, 3)} ${numbers.slice(3, 6)} ${numbers.slice(6, 8)} ${numbers.slice(8)}`;

        input.value = formatted;
    });

});
