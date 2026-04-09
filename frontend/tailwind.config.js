import { colors } from "./src/theme/colors";

export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        primary: colors.primary,
        secondary: colors.secondary,
        background: colors.background,
        surface: colors.surface,
        card: colors.card,
        text: colors.text,
        border: colors.border,
        error: colors.error,
      },
    },
  },
  plugins: [],
};