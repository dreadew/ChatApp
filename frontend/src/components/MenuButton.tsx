import { Button } from './ui/button'

type Props = {
  text: React.ReactNode
  icon: React.ReactNode
  action: () => void
}

export const MenuButton = ({ text, icon, action }: Props) => {
  return <Button onClick={action} variant={'link'}>
      <div className='flex flex-col items-center gap-2'>
        {icon}
        <p className='text-xs'>{text}</p>
      </div>
    </Button>
}