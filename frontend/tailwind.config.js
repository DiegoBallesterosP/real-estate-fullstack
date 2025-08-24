/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{ts,tsx,js,jsx}"
  ],
  theme: {
    container: {
      center: true,
      padding: {
        DEFAULT: "1rem",
        md: "2rem",
        lg: "4rem"
      }
    },
    extend: {
      colors: {
        primary: "#1D4ED8",
        secondary: "#FBBF24",
        text: "#1F2937",
        inputBg: "#F3F4F6"
      },
      fontFamily: {
        sans: ["Inter", "sans-serif"]
      },
      backgroundImage: {
        "brand-gradient": "linear-gradient(to bottom, #DBEAFE, #FFFFFF)"
      }
    }
  },
  plugins: []
}