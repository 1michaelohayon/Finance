import useField from "../hooks/useField";
import { NewLiability } from "../types";
import { useAuth0 } from "@auth0/auth0-react";
import parse from "../utils/parse";
import liabilitiesService from "../services/liabilities";

const CreateLiability = () => {
  const name = useField("text");
  const amount = useField("number");
  const { getAccessTokenSilently } = useAuth0();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const newLiability: NewLiability = {
      name: name.input.value,
      amount: Number(amount.input.value),
    };

    const token = parse.str(await getAccessTokenSilently());
    console.log(newLiability);
    const returned = await liabilitiesService.create({
      liability: newLiability,
      token,
    });
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
