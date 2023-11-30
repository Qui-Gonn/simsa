/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
  theme: {
      extend: {
          gridTemplateRows: {
              'aa1': 'auto auto 1fr',
          },
      },
  },
  plugins: [],
}

