import { createRoot } from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import './index.css'
import { MainPage } from './components/MainPage'
import { SignInLink } from './constants/links/sign-in-link'
import { SignInPage } from './components/SignInPage'
import { SignUpLink } from './constants/links/sign-up-link'
import { SignUpPage } from './components/SignUpPage'
import ErrorPage from './components/ErrorPage'
import { AuthProvider } from './contexts/auth-context'
import { ChatsPage } from './components/ChatsPage'
import { UsersPage } from './components/UsersPage'
import { ChatRoom } from './components/ChatRoom'

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <ErrorPage />
  },
  {
    path: "/users",
    element: <UsersPage />,
    errorElement: <ErrorPage />
  },
  {
    path: "/chats",
    element: <ChatsPage />,
    errorElement: <ErrorPage />
  },
  {
    path: '/chat/:id',
    element: <ChatRoom />,
    errorElement: <ErrorPage />
  },
  {
    path: SignInLink,
    element: <SignInPage />,
    errorElement: <ErrorPage />
  },
  {
    path: SignUpLink,
    element: <SignUpPage />,
    errorElement: <ErrorPage />
  }
])

createRoot(document.getElementById('root')!).render(
  <AuthProvider>
    <RouterProvider router={router} />
  </AuthProvider>
)
