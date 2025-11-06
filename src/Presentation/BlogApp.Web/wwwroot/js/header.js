function initMobileMenu() {
    const mobileMenuButton = document.getElementById('mobile-menu-button');
    const mobileMenu = document.getElementById('mobile-menu');

    if (mobileMenuButton && mobileMenu) {
        // Önceki event listener'ları temizle
        mobileMenuButton.replaceWith(mobileMenuButton.cloneNode(true));
        const newButton = document.getElementById('mobile-menu-button');

        newButton.addEventListener('click', function () {
            const icon = this.querySelector('.material-symbols-outlined');
            const isClosed = mobileMenu.classList.contains('max-h-0');

            if (isClosed) {
                mobileMenu.classList.remove('max-h-0', 'opacity-0', 'scale-y-95');
                mobileMenu.classList.add('max-h-[600px]', 'opacity-100', 'scale-y-100');
                if (icon) icon.textContent = 'close';
            } else {
                mobileMenu.classList.add('max-h-0', 'opacity-0', 'scale-y-95');
                mobileMenu.classList.remove('max-h-[600px]', 'opacity-100', 'scale-y-100');
                if (icon) icon.textContent = 'menu';
            }
        });
    }
}

// DOM hazır olduğunda çalıştır
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initMobileMenu);
} else {
    initMobileMenu();
}

// ViewComponent yüklendikten sonra da çalıştır
const observer = new MutationObserver(function (mutations) {
    mutations.forEach(function (mutation) {
        if (mutation.type === 'childList') {
            const mobileMenuButton = document.getElementById('mobile-menu-button');
            if (mobileMenuButton) {
                initMobileMenu();
                observer.disconnect();
            }
        }
    });
});

observer.observe(document.body, { childList: true, subtree: true });