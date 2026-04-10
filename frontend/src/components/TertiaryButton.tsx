import { colors } from "../theme/colors";

type Props = {
  title: string;
  onClick: () => void;
};

export default function TertiaryButton({ title, onClick }: Props) {
  return (
    <button
      onClick={onClick}
      className="text-sm font-medium transition-opacity active:opacity-60"
      style={{ color: colors.secondary }}
    >
      {title}
    </button>
  );
}
