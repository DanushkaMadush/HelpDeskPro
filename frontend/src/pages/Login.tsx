import { useState } from "react";
import { colors } from "../theme/colors";
import PrimaryButton from "../components/PrimaryButton";
import SecondaryButton from "../components/SecondaryButton";
import TertiaryButton from "../components/TertiaryButton";

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
        <div className="flex justify-center mb-6">
          <img
            src="/HelpDeskProLogo.jpg"
            alt="HelpDeskPro Logo"
            className="w-32 h-32 object-contain"
          />
        </div>

        <div className="mb-4">
          <label className="block mb-1 text-sm" style={{ color: colors.text }}>
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

        <div className="mb-6">
          <label className="block mb-1 text-sm" style={{ color: colors.text }}>
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

        <PrimaryButton
          title={loading ? "Logging in..." : "Login"}
          onClick={handleLogin}
          disabled={loading}
        />

        <div className="h-3" />

        <SecondaryButton title="Signup" onClick={() => {}} />

        <div className="h-3" />

        <div className="flex justify-center">
          <TertiaryButton title="Forgot password?" onClick={() => {}} />
        </div>
      </div>
    </div>
  );
}