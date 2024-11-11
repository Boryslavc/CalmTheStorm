using TMPro;


namespace Utilities
{
    public class ObervableValue<T> 
    {
        public ObervableValue(TMP_Text image)
        {
            valueDispayer = image;
        }

        private TMP_Text valueDispayer;

        private T value;
        public T Value {
            get {  return value; }
            set
            {
                this.value = value;
                valueDispayer.text = value.ToString();
            } 
        }
    }
}

