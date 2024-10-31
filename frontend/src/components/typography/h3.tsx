import { cn } from '../../lib/utils'

type Props = {
	text: string | React.ReactNode
	className?: string
}

export const H3 = ({ text, className }: Props) => {
	return (
		<h3
			className={cn(
				'scroll-m-20 text-2xl font-semibold tracking-tight text-foreground',
				className
			)}
		>
			{text}
		</h3>
	)
}
