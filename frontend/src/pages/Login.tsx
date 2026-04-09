import { useState } from "react";
import { colors } from "../theme/colors";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const handleLogin = () => {
    if (!email || !password) {
      alert("Please enter email and password");
      return;
    }

    setLoading(true);

    // API integration later
    setTimeout(() => {
      setLoading(false);
      alert("Login clicked");
    }, 1000);
  };

  return (
    <div
      className="min-h-screen flex items-center justify-center"
      style={{ backgroundColor: colors.background }}
    >
      <div
        className="w-full max-w-md p-8 rounded-2xl shadow-lg"
        style={{ backgroundColor: colors.card }}
      >
        {/* Logo */}
        <div className="flex justify-center mb-6">
          <img
            src="/HelpDeskProLogo.jpg" // <-- place your logo in public folder
            alt="HelpDeskPro Logo"
            className="w-32 h-32 object-contain"
          />
        </div>

        {/* Title */}
        <h2
          className="text-2xl font-semibold text-center mb-6"
          style={{ color: colors.text }}
        >
          Login to HelpDeskPro
        </h2>

        {/* Email */}
        <div className="mb-4">
          <label
            className="block mb-1 text-sm"
            style={{ color: colors.text }}
          >
            Email
          </label>
          <input
            type="email"
            placeholder="Enter your email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full p-3 rounded-lg outline-none"
            style={{
              backgroundColor: colors.surface,
              color: colors.text,
              border: `1px solid ${colors.border}`,
            }}
          />
        </div>

        {/* Password */}
        <div className="mb-6">
          <label
            className="block mb-1 text-sm"
            style={{ color: colors.text }}
          >
            Password
          </label>
          <input
            type="password"
            placeholder="Enter your password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full p-3 rounded-lg outline-none"
            style={{
              backgroundColor: colors.surface,
              color: colors.text,
              border: `1px solid ${colors.border}`,
            }}
          />
        </div>

        {/* Login Button */}
        <button
          onClick={handleLogin}
          disabled={loading}
          className="w-full py-3 rounded-lg font-semibold transition"
          style={{
            backgroundColor: colors.primary,
            color: "#fff",
            opacity: loading ? 0.7 : 1,
          }}
        >
          {loading ? "Logging in..." : "Login"}
        </button>

        {/* Extra Actions */}
        <div className="mt-4 flex justify-between text-sm">
          <button
            className="hover:underline"
            style={{ color: colors.secondary }}
          >
            Signup
          </button>

          <button
            className="hover:underline"
            style={{ color: colors.secondary }}
          >
            Forgot password?
          </button>
        </div>
      </div>
    </div>
  );
}