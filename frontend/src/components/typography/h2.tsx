import { cn } from '../../lib/utils'

type Props = {
	text: string | React.ReactNode
	className?: string
}

export const H2 = ({ text, className }: Props) => {
	return (
		<h2
			className={cn(
				'scroll-m-20 text-3xl font-semibold tracking-tight text-foreground',
				className
			)}
		>
			{text}
		</h2>
	)
}
