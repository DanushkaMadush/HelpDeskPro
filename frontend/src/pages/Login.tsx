import { useState } from "react";
import { colors } from "../theme/colors";
import PrimaryButton from "../components/PrimaryButton";
import SecondaryButton from "../components/SecondaryButton";
import TertiaryButton from "../components/TertiaryButton";
import { login } from "../api/auth.api";
import { saveToken } from "../utils/tokenStorage";
import toast from "react-hot-toast";
import { getUserRole } from "../utils/jwt.service";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

const handleLogin = async () => {
  if (!email || !password) {
    toast.error("Please enter email and password");
    return;
  }

  setLoading(true);

  try {
    const response = await login({ email, password });

    saveToken(response.token);

    const role = await getUserRole();

    if (!role) {
      toast.error("Role not found");
      return;
    }

    if (role === "manager") {
      navigate("/dashboard");
    } else if (role === "admin") {
      navigate("/admin");
    } else {
      toast.error("Unauthorized role");
    }

    toast.success("Login successful!");
  } catch (error: any) {
    let message = "Login failed";

    if (!error.response) {
      message = "Network error";
    } else if (error.response.status === 401) {
      message = "Invalid email or password";
    } else if (error.response.data?.message) {
      message = error.response.data.message;
    }

    toast.error(message);
  } finally {
    setLoading(false);
  }
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