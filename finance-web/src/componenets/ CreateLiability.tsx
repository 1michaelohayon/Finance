import useField from "../hooks/useField";
import { NewLiability, User } from "../types";
import { useAuth0 } from "@auth0/auth0-react";
import parse from "../utils/parse";
import liabilitiesService from "../services/liabilities";

const CreateLiability = () => {
  const name = useField("text");
  const amount = useField("number");
  const { getIdTokenClaims } = useAuth0();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    const claims = await getIdTokenClaims();
    event.preventDefault();
    const user: User = {
      id: parse.str(claims?.sub),
      email: parse.str(claims?.email),
      emailVerified: parse.bool(claims?.email_verified),
      name: parse.str(claims?.name),
      username: parse.str(claims?.nickname),
      picture: parse.str(claims?.picture),
    };
    const newLiability: NewLiability = {
      name: name.input.value,
      amount: Number(amount.input.value),
      user: user,
    };

    console.log(newLiability);
    const returned = await liabilitiesService.create(newLiability);
    console.log("returned", returned);
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div>
          name
          <input {...name.input} />
        </div>
        <div>
          amount
          <input {...amount.input} />
        </div>
        <button type="submit">create</button>
      </form>
    </div>
  );
};

export default CreateLiability;
