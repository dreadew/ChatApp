import { SignInLink } from '../../constants/links/sign-in-link'
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
import { Link, useNavigate } from "react-router-dom";
import userService from '../../services/user.service'
import { useState } from 'react'

const formSchema = z.object({
	email: z.string().email(),
	username: z.string().min(4).max(16),
	password: z.string().min(4).max(16),
})

type Props = {}

export const SignUpForm = ({}: Props) => {
	const [isLoading, setIsLoading] = useState<boolean>(false)
	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
		defaultValues: {
			email: '',
			username: '',
			password: '',
		},
	})
	const navigate = useNavigate()

	const onSubmit = async (values: z.infer<typeof formSchema>) => {
		setIsLoading(true)
		try {
			await userService.SignUp(values)
			navigate(SignInLink)
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
						name='email'
						render={({ field }) => (
							<FormItem>
								<FormLabel>Эл. почта</FormLabel>
								<FormControl>
									<Input
										disabled={isLoading}
										autoFocus
										error={form.formState.errors?.username?.message}
										placeholder='john.doe@example.com'
										{...field}
									/>
								</FormControl>
								<FormMessage />
							</FormItem>
						)}
					/>
					<FormField
						control={form.control}
						name='username'
						render={({ field }) => (
							<FormItem>
								<FormLabel>Имя пользователя</FormLabel>
								<FormControl>
									<Input
										disabled={isLoading}
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
								<FormLabel>Пароль</FormLabel>
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
				<Button disabled={isLoading} type='submit'>Зарегистрироваться</Button>
				<span className='flex gap-1 text-sm text-muted-foreground self-center'>
					Уже есть аккаунт?{' '}
					<Link className='font-medium text-primary' to={SignInLink}>
						Войти
					</Link>
				</span>
			</form>
		</Form>
	)
}
