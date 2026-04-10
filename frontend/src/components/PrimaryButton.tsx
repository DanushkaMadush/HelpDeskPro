import { colors } from "../theme/colors";

type Props = {
  title: string;
  onClick: () => void;
  disabled?: boolean;
};

export default function PrimaryButton({
  title,
  onClick,
  disabled = false,
}: Props) {
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      className="w-full py-3 rounded-lg text-base font-semibold transition-opacity active:opacity-85 disabled:opacity-50"
      style={{
        backgroundColor: colors.primary,
        color: "#FFFFFF",
      }}
    >
      {title}
    </button>
  );
}
