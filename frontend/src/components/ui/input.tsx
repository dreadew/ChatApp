import * as React from 'react'

import { cn } from '../../lib/utils'
import { EyeClosedIcon, EyeOpenIcon } from '@radix-ui/react-icons'
import { Button } from './button'

export interface InputProps
	extends React.InputHTMLAttributes<HTMLInputElement> {
	error?: string
	state?: boolean
	setState?: (state: boolean) => void
}

const Input = React.forwardRef<HTMLInputElement, InputProps>(
	({ className, type, error, state, setState, ...props }, ref) => {
		return (
			<div className='w-full relative'>
				<input
					type={type === 'password' ? (state ? 'text' : 'password') : type}
					className={cn(
						'flex h-9 w-full font-medium text-foreground rounded-md border border-accent bg-transparent px-3 py-1 text-sm shadow-sm transition-all file:border-0 file:bg-transparent file:text-sm file:font-medium file:text-foreground placeholder:text-accent-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-1 focus-visible:ring-primary focus-visible:border-accent-foreground/50 disabled:cursor-not-allowed disabled:opacity-50',
						error &&
							'bg-destructive border-destructive-foreground text-destructive-foreground',
						className
					)}
					ref={ref}
					{...props}
				/>
				{type === 'password' && setState && typeof state !== 'undefined' && (
					<Button
						className={cn(
							'absolute right-0 top-1/2 -translate-y-1/2 h-full rounded-r-md rounded-l-none',
							error &&
								'bg-destructive border-destructive-foreground text-destructive-foreground'
						)}
						variant={'outline'}
						onClick={() => setState(!state)}
						type='button'
					>
						{state ? (
							<EyeClosedIcon
								className={cn(
									'h-5 w-5 text-accent-foreground',
									error && 'text-destructive-foreground'
								)}
							/>
						) : (
							<EyeOpenIcon
								className={cn(
									'h-5 w-5 text-accent-foreground',
									error && 'text-destructive-foreground'
								)}
							/>
						)}
					</Button>
				)}
			</div>
		)
	}
)

Input.displayName = 'Input'

const PasswordInput = React.forwardRef<HTMLInputElement, InputProps>(
	({ ...props }, ref) => {
		const [isShown, setIsShown] = React.useState<boolean>(false)
		return (
			<Input
				state={isShown}
				setState={setIsShown}
				type='password'
				ref={ref}
				{...props}
			/>
		)
	}
)

PasswordInput.displayName = 'PasswordInput'

export { Input, PasswordInput }
