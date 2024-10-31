import { useRouteError } from "react-router-dom";
import { P } from './typography/p'
import { Code } from './typography/code'
import { H2 } from './typography/h2'

export default function ErrorPage() {
  const error = useRouteError() as any;
  console.error(error);

  return (
    <div className='h-screen flex flex-col gap-2 items-center justify-center' id="error-page">
      <H2 text={"Oops!"} />
      <P text={"Sorry, an unexpected error has occurred."}></P>
      <Code text={error.statusText || error.message} />
    </div>
  );
}
