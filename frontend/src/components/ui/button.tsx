import { cva, type VariantProps } from 'class-variance-authority'
import * as React from 'react'

import { cn } from '../../lib/utils'

const buttonVariants = cva(
	'text-foreground inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium transition-[colors,transform] focus-visible:outline-none disabled:pointer-events-none disabled:opacity-50 active:ring-offset-1 active:ring-2 active:ring-foreground active:scale-95',
	{
		variants: {
			variant: {
				default:
					'bg-primary text-primary-foreground shadow betterhover:hover:bg-primary/90 active:ring-primary',
				outline:
					'border border-accent bg-background shadow-sm betterhover:hover:bg-accent/25',
				secondary:
					'bg-secondary text-secondary-foreground betterhover:hover:bg-secondary/80',
				ghost:
					'betterhover:hover:bg-accent betterhover:hover:text-accent-foreground',
				link: 'text-accent-foreground active:ring-0 active:ring-offset-0 betterhover:hover:opacity-80',
			},
			size: {
				default: 'h-9 px-4 py-2',
				sm: 'h-8 rounded-md px-3 text-xs',
				lg: 'h-10 rounded-md px-8',
				icon: 'h-9 w-9',
			},
		},
		defaultVariants: {
			variant: 'default',
			size: 'default',
		},
	}
)

export interface ButtonProps
	extends React.ButtonHTMLAttributes<HTMLButtonElement>,
		VariantProps<typeof buttonVariants> {}

const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(
	({ className, variant, size, ...props }, ref) => {
		return (
			<button
				className={cn(buttonVariants({ variant, size, className }))}
				ref={ref}
				{...props}
			/>
		)
	}
)
Button.displayName = 'Button'

export { Button, buttonVariants }
