/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.cshtml",
        "./Pages/**/*.cshtml",
        "./wwwroot/**/*.js"
    ],
    darkMode: 'class', // dark mode için
    theme: {
        extend: {
            colors: {
                primary: "#003366",
                "primary-dark": "#001f3f",
                "accent-gray": "#6c757d",
                "background-light": "#f4f4f4",
                "background-dark": "#101622",
            },
            fontFamily: {
                display: ["Open Sans", "sans-serif"],
            },
        }
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('@tailwindcss/container-queries')
    ],
}