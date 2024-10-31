import { cn } from '../../lib/utils'

type Props = {
	text: string | React.ReactNode
	className?: string
}

export const Blockquote = ({ text, className }: Props) => {
	return (
		<blockquote
			className={cn(
				'mt-6 border-l-2 border-accent pl-6 italic text-foreground',
				className
			)}
		>
			{text}
		</blockquote>
	)
}
