import { colors } from "../theme/colors";

type Props = {
  title: string;
  onClick: () => void;
};

export default function SecondaryButton({ title, onClick }: Props) {
  return (
    <button
      onClick={onClick}
      className="w-full py-3 rounded-lg border-2 text-base font-semibold transition-opacity active:opacity-80"
      style={{
        borderColor: colors.secondary,
        color: colors.secondary,
        backgroundColor: "transparent",
      }}
    >
      {title}
    </button>
  );
}
