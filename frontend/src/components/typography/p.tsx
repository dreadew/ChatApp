import { cn } from '../../lib/utils'

type Props = {
	text: string | React.ReactNode
	className?: string
}

export const P = ({ text, className }: Props) => {
	return (
		<p
			className={cn(
				'leading-7 text-md text-accent-foreground font-medium',
				className
			)}
		>
			{text}
		</p>
	)
}
