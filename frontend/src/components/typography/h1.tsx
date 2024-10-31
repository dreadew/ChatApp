import { cn } from '../../lib/utils'

type Props = {
	text: string | React.ReactNode
	className?: string
}

export const H1 = ({ text, className }: Props) => {
	return (
		<h1
			className={cn(
				'scroll-m-20 text-4xl font-semibold tracking-tight md:text-5xl lg:text-6xl text-foreground',
				className
			)}
		>
			{text}
		</h1>
	)
}
