function [ predictions ] = make_predictions(weights, probes)

probes_size = size(probes, 2) - 1;
predictions = zeros(probes_size, 1);
for i = 1:probes_size
    predictions(i) = sum(weights .* probes(:,i));
end

end

