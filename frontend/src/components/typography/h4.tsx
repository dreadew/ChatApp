import { cn } from '../../lib/utils'

type Props = {
	text: string | React.ReactNode
	className?: string
}

export const H4 = ({ text, className }: Props) => {
	return (
		<h4
			className={cn(
				'scroll-m-20 text-xl font-semibold tracking-tight text-foreground',
				className
			)}
		>
			{text}
		</h4>
	)
}
