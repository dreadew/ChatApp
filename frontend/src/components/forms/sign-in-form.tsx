import { SignUpLink } from '../../constants/links/sign-up-link'
import { zodResolver } from '@hookform/resolvers/zod'
import { useForm } from 'react-hook-form'
import { z } from 'zod'
import { Button } from '../ui/button'
import {
	Form,
	FormControl,
	FormField,
	FormItem,
	FormLabel,
	FormMessage,
} from '../ui/form'
import { Input, PasswordInput } from '../ui/input'
import { Link } from "react-router-dom";
import { useState } from 'react'
import userService from '../../services/user.service'
import { SetCookie } from '../../services/cookies.service'
import { AccessTokenCookie } from '../../constants/cookies/access-token-cookie'

const formSchema = z.object({
	username: z.string().min(4).max(16),
	password: z.string().min(4).max(16),
})

type Props = {}

export const SignInForm = ({}: Props) => {
	const [isLoading, setIsLoading] = useState<boolean>(false)
	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
		defaultValues: {
			username: '',
			password: '',
		},
	})

	const onSubmit = async (values: z.infer<typeof formSchema>) => {
		setIsLoading(true)
		try {
			const res = await userService.SignIn(values)
			SetCookie(AccessTokenCookie, res.data.data.accessToken)
			window.location.reload()
		} catch (err: unknown) {
			console.error(err)
		} finally {
			setIsLoading(false)
		}
	}

	return (
		<Form {...form}>
			<form
				onSubmit={form.handleSubmit(onSubmit)}
				className='max-w-[30rem] w-full flex flex-col gap-3'
			>
				<div className='flex flex-col gap-2'>
					<FormField
						control={form.control}
						name='username'
						render={({ field }) => (
							<FormItem>
								<FormLabel>Имя пользователя или эл. почта</FormLabel>
								<FormControl>
									<Input
										disabled={isLoading}
										autoFocus
										error={form.formState.errors?.username?.message}
										placeholder='john.doe'
										{...field}
									/>
								</FormControl>
								<FormMessage />
							</FormItem>
						)}
					/>
					<FormField
						control={form.control}
						name='password'
						render={({ field }) => (
							<FormItem>
								<div className='flex items-center justify-between'>
									<FormLabel>Пароль</FormLabel>
								</div>
								<FormControl>
									<PasswordInput
										disabled={isLoading}
										error={form.formState.errors?.password?.message}
										type='password'
										placeholder='******'
										{...field}
									/>
								</FormControl>
								<FormMessage />
							</FormItem>
						)}
					/>
				</div>
				<Button disabled={isLoading} type='submit'>Войти в аккаунт</Button>
				<span className='flex gap-1 text-sm text-muted-foreground self-center'>
					Еще нет аккаунта?{' '}
					<Link className='font-medium text-primary' to={SignUpLink}>
						Зарегистрироваться
					</Link>
				</span>
			</form>
		</Form>
	)
}
