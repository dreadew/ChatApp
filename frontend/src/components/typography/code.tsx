import { cn } from '../../lib/utils'

type Props = {
	text: string | React.ReactNode
	className?: string
}

export const Code = ({ text, className }: Props) => {
	return (
		<code
			className={cn(
				'relative rounded bg-accent px-[0.3rem] py-[0.2rem] font-mono text-sm font-semibold text-foreground',
				className
			)}
		>
			{text}
		</code>
	)
}
